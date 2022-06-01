using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationServices.Interfaces;
using EntitiesServices.Model;
using System.Globalization;
using SMS_Presentation.App_Start;
using EntitiesServices.WorkClasses;
using AutoMapper;
using ERP_CRM_Solution.ViewModels;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using System.Web.UI.WebControls;
using System.Runtime.Caching;
using Image = iTextSharp.text.Image;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Canducci.Zip;
using CrossCutting;

namespace Presentation_EcoAssist.Controllers
{
    public class PrestadorController : Controller
    {
        private readonly IPrestadorAppService baseApp;
        private readonly ILogAppService logApp;
        private readonly IUsuarioAppService usuApp;
        private readonly IConfiguracaoAppService confApp;

        private String msg;
        private Exception exception;
        PRESTADOR objeto = new PRESTADOR();
        PRESTADOR objetoAntes = new PRESTADOR();
        List<PRESTADOR> listaMaster = new List<PRESTADOR>();
        String extensao;

        public PrestadorController(IPrestadorAppService baseApps, ILogAppService logApps, IUsuarioAppService usuApps, IConfiguracaoAppService confApps)
        {
            baseApp = baseApps;
            logApp = logApps;
            usuApp = usuApps;
            confApp = confApps;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            return View();
        }

        public ActionResult Voltar()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            return RedirectToAction("MontarTelaDashboardCadastros", "BaseAdmin");
        }

        public ActionResult VoltarGeral()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            return RedirectToAction("CarregarBase", "BaseAdmin");
        }

        public ActionResult EnviarSmsCliente(Int32 id, String mensagem)
        {
            try
            {
                PRESTADOR clie = baseApp.GetById(id);
                USUARIO_SUGESTAO usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];

                // Verifica existencia prévia
                if (clie == null)
                {
                    Session["MensSMSClie"] = 1;
                    return RedirectToAction("MontarTelaPrestador");
                }

                // Criticas
                if (clie.PRES_NR_CELULAR_PRINCIPAL == null)
                {
                    Session["MensSMSClie"] = 2;
                    return RedirectToAction("MontarTelaPrestador");
                }

                // Monta token
                CONFIGURACAO conf = confApp.GetItemById(1);
                String text = conf.CONF_SG_LOGIN_SMS + ":" + conf.CONF_SG_SENHA_SMS;
                byte[] textBytes = Encoding.UTF8.GetBytes(text);
                String token = Convert.ToBase64String(textBytes);
                String auth = "Basic " + token;

                // Prepara texto
                String texto = mensagem;

                // Prepara corpo do SMS e trata link
                StringBuilder str = new StringBuilder();
                str.AppendLine(texto);
                String body = str.ToString();
                String smsBody = body;

                // inicia processo
                String resposta = String.Empty;
                try
                {
                    String listaDest = "55" + Regex.Replace(clie.PRES_NR_CELULAR_PRINCIPAL, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled).ToString();
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api-v2.smsfire.com.br/sms/send/bulk");
                    httpWebRequest.Headers["Authorization"] = auth;
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    String customId = Cryptography.GenerateRandomPassword(8);
                    String data = String.Empty;
                    String json = String.Empty;

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        json = String.Concat("{\"destinations\": [{\"to\": \"", listaDest, "\", \"text\": \"", texto, "\", \"customId\": \"" + customId + "\", \"from\": \"EcoAssist\"}]}");
                        streamWriter.Write(json);
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        resposta = result;
                    }
                }
                catch (Exception ex)
                {
                    String erro = ex.Message;
                }

                Session["MensSMSClie"] = 200;
                return RedirectToAction("MontarTelaPrestador");
            }
            catch (Exception ex)
            {
                Session["MensSMSClie"] = 3;
                Session["MensSMSClieErro"] = ex.Message;
                return RedirectToAction("MontarTelaPrestador");
            }
        }

        [HttpPost]
        public JsonResult BuscaNomeRazao(String nome)
        {
            Int32 isRazao = 0;
            List<Hashtable> listResult = new List<Hashtable>();
            List<PRESTADOR> pres = baseApp.GetAllItens();
            Session["Prestadores"] = pres;

            if (nome != null)
            {
                List<PRESTADOR> lstPres = pres.Where(x => x.PRES_NM_NOME != null && x.PRES_NM_NOME.ToLower().Contains(nome.ToLower())).ToList<PRESTADOR>();

                if (lstPres == null || lstPres.Count == 0)
                {
                    isRazao = 1;
                    lstPres = pres.Where(x => x.PRES_NM_RAZAO_SOCIAL != null).ToList<PRESTADOR>();
                    lstPres = lstPres.Where(x => x.PRES_NM_RAZAO_SOCIAL.ToLower().Contains(nome.ToLower())).ToList<PRESTADOR>();
                }

                if (lstPres != null)
                {
                    foreach (var item in lstPres)
                    {
                        Hashtable result = new Hashtable();
                        result.Add("id", item.PRES_CD_ID);
                        if (isRazao == 0)
                        {
                            result.Add("text", item.PRES_NM_NOME);
                        }
                        else
                        {
                            result.Add("text", item.PRES_NM_NOME + " (" + item.PRES_NM_RAZAO_SOCIAL + ")");
                        }
                        listResult.Add(result);
                    }
                }
            }
            return Json(listResult);
        }

        public void FlagContinua()
        {
            Session["VoltaCliente"] = 3;
        }

        [HttpPost]
        public JsonResult PesquisaCNPJ(string cnpj)
        {
            List<PRESTADOR_QUADRO_SOCIETARIO> lstQs = new List<PRESTADOR_QUADRO_SOCIETARIO>();

            var url = "https://api.cnpja.com.br/companies/" + Regex.Replace(cnpj, "[^0-9]", "");
            String json = String.Empty;
            WebRequest request = WebRequest.Create(url);
            request.Headers["Authorization"] = "df3c411d-bb44-41eb-9304-871c45d72978-cd751b62-ff3d-4421-a9d2-b97e01ca6d2b";

            try
            {
                WebResponse response = request.GetResponse();
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), ASCIIEncoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }
                var jObject = JObject.Parse(json);

                if (jObject["membership"].Count() == 0)
                {
                    PRESTADOR_QUADRO_SOCIETARIO qs = new PRESTADOR_QUADRO_SOCIETARIO();

                    qs.PRESTADOR = new PRESTADOR();
                    qs.PRESTADOR.PRES_NM_RAZAO_SOCIAL = jObject["name"] == null ? String.Empty : jObject["name"].ToString();
                    qs.PRESTADOR.PRES_NM_NOME = jObject["alias"] == null ? jObject["name"].ToString() : jObject["alias"].ToString();
                    qs.PRESTADOR.PRES_NR_TELEFONE_PRINCIPAL = jObject["phone"].ToString();
                    qs.PRESTADOR.PRES_EM_EMAIL_PRINCIPAL = jObject["email"].ToString();
                    qs.PRQS_IN_ATIVO = 0;
                    lstQs.Add(qs);
                }
                else
                {
                    foreach (var s in jObject["membership"])
                    {
                        PRESTADOR_QUADRO_SOCIETARIO qs = new PRESTADOR_QUADRO_SOCIETARIO();

                        qs.PRESTADOR = new PRESTADOR();
                        qs.PRESTADOR.PRES_NM_RAZAO_SOCIAL = jObject["name"].ToString() == "" ? String.Empty : jObject["name"].ToString();
                        qs.PRESTADOR.PRES_NM_NOME = jObject["alias"].ToString() == "" ? jObject["name"].ToString() : jObject["alias"].ToString();
                        qs.PRESTADOR.PRES_NR_TELEFONE_PRINCIPAL = jObject["phone"].ToString();
                        qs.PRESTADOR.PRES_EM_EMAIL_PRINCIPAL = jObject["email"].ToString();
                        qs.PRQS_NM_QUALIFICACAO = s["role"]["description"].ToString();
                        qs.PRQS_NM_NOME = s["name"].ToString();

                        // CNPJá não retorna esses valores
                        qs.PRQS_NM_PAIS_ORIGEM = String.Empty;
                        qs.PRQS_NM_REPRESENTANTE_LEGAL = String.Empty;
                        qs.PRQS_NM_QUALIFICACAO_REP_LEGAL = String.Empty;
                        lstQs.Add(qs);
                    }
                }
                return Json(lstQs);
            }
            catch (WebException ex)
            {
                var hash = new Hashtable();
                hash.Add("status", "ERROR");

                if ((ex.Response as HttpWebResponse)?.StatusCode.ToString() == "BadRequest")
                {
                    hash.Add("public", 1);
                    hash.Add("message", "CNPJ inválido");
                    return Json(hash);
                }
                if ((ex.Response as HttpWebResponse)?.StatusCode.ToString() == "NotFound")
                {
                    hash.Add("public", 1);
                    hash.Add("message", "O CNPJ consultado não está registrado na Receita Federal");
                    return Json(hash);
                }
                else
                {
                    hash.Add("public", 1);
                    hash.Add("message", ex.Message);
                    return Json(hash);
                }
            }
        }

        private List<PRESTADOR_QUADRO_SOCIETARIO> PesquisaCNPJ(PRESTADOR cliente)
        {
            List<PRESTADOR_QUADRO_SOCIETARIO> lstQs = new List<PRESTADOR_QUADRO_SOCIETARIO>();

            var url = "https://api.cnpja.com.br/companies/" + Regex.Replace(cliente.PRES_NR_CNPJ, "[^0-9]", "");
            String json = String.Empty;

            WebRequest request = WebRequest.Create(url);
            request.Headers["Authorization"] = "df3c411d-bb44-41eb-9304-871c45d72978-cd751b62-ff3d-4421-a9d2-b97e01ca6d2b";

            WebResponse response = request.GetResponse();

            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), ASCIIEncoding.UTF8))
            {
                json = reader.ReadToEnd();
            }

            var jObject = JObject.Parse(json);

            foreach (var s in jObject["membership"])
            {
                PRESTADOR_QUADRO_SOCIETARIO qs = new PRESTADOR_QUADRO_SOCIETARIO();

                qs.PRQS_NM_QUALIFICACAO = s["role"]["description"].ToString();
                qs.PRQS_NM_NOME = s["name"].ToString();
                qs.PRES_CD_ID = cliente.PRES_CD_ID;

                // CNPJá não retorna esses valores
                qs.PRQS_NM_PAIS_ORIGEM = String.Empty;
                qs.PRQS_NM_REPRESENTANTE_LEGAL = String.Empty;
                qs.PRQS_NM_QUALIFICACAO_REP_LEGAL = String.Empty;

                lstQs.Add(qs);
            }
            return lstQs;
        }










































    }
}