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
        private readonly IPrestadorCnpjAppService pcnpjApp;

        private String msg;
        private Exception exception;
        PRESTADOR objeto = new PRESTADOR();
        PRESTADOR objetoAntes = new PRESTADOR();
        List<PRESTADOR> listaMaster = new List<PRESTADOR>();
        String extensao;

        public PrestadorController(IPrestadorAppService baseApps, ILogAppService logApps, IUsuarioAppService usuApps, IConfiguracaoAppService confApps, IPrestadorCnpjAppService pcnpjApps)
        {
            baseApp = baseApps;
            logApp = logApps;
            usuApp = usuApps;
            confApp = confApps;
            pcnpjApp = pcnpjApps;
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

        [HttpGet]
        public ActionResult MontarTelaPrestador()
        {
            // Verifica se tem usuario logado
            USUARIO_SUGESTAO usuario = new USUARIO_SUGESTAO();
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if ((USUARIO_SUGESTAO)Session["UserCredentials"] != null)
            {
                usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];

                // Verfifica permissão
                if (usuario.PERFIL.PERF_SG_SIGLA == "VIS")
                {
                    Session["MensPermissao"] = 2;
                    return RedirectToAction("CarregarBase", "BaseAdmin");
                }
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idAss = (Int32)Session["IdAssinante"];

            // Carrega listas
            if ((List<PRESTADOR>)Session["ListaPrestador"] == null || ((List<PRESTADOR>)Session["ListaPrestador"]).Count == 0)
            {
                listaMaster = baseApp.GetAllItens();
                Session["ListaPrestador"] = listaMaster;
            }
            ViewBag.Listas = (List<PRESTADOR>)Session["ListaPrestador"];
            ViewBag.Title = "Prestador";
            Session["Prestador"] = null;
            Session["IncluirPrestador"] = 0;

            // Indicadores
            ViewBag.Prestadores = ((List<PRESTADOR>)Session["ListaPrestador"]).Count;
            ViewBag.Perfil = usuario.PERFIL.PERF_SG_SIGLA;

            if (Session["MensPrestador"] != null)
            {
                // Mensagem
                if ((Int32)Session["MensPrestador"] == 2)
                {
                    ModelState.AddModelError("", SMS_Mensagens.ResourceManager.GetString("M0011", CultureInfo.CurrentCulture));
                }
                if ((Int32)Session["MensPrestador"] == 3)
                {
                    ModelState.AddModelError("", SMS_Mensagens.ResourceManager.GetString("M0029", CultureInfo.CurrentCulture));
                }
                if ((Int32)Session["MensPrestador"] == 4)
                {
                    ModelState.AddModelError("", SMS_Mensagens.ResourceManager.GetString("M0030", CultureInfo.CurrentCulture));
                }
                if ((Int32)Session["MensPrestador"] == 50)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0080", CultureInfo.CurrentCulture));
                }
            }

            // Abre view
            Session["MensPrestador"] = 0;
            Session["VoltaPrestador"] = 1;
            objeto = new PRESTADOR();
            if (Session["FiltroPrestador"] != null)
            {
                objeto = (PRESTADOR)Session["FiltroPrestador"];
            }
            objeto.PRES_IN_POSSUI_SEGURO = 1;
            return View(objeto);
        }

        public ActionResult RetirarFiltroPrestador()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Session["ListaPrestador"] = null;
            Session["FiltroPrestador"] = null;
            if ((Int32)Session["VoltaPrestador"] == 2)
            {
                return RedirectToAction("VerCardsPrestador");
            }
            return RedirectToAction("MontarTelaPrestador");
        }

        public ActionResult MostrarTudoPrestador()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idAss = (Int32)Session["IdAssinante"];
            listaMaster = baseApp.GetAllItensAdm();
            Session["FiltroPrestador"] = null;
            Session["ListaPrestador"] = listaMaster;
            if ((Int32)Session["VoltaPrestador"] == 2)
            {
                return RedirectToAction("VerCardsPrestador");
            }
            return RedirectToAction("MontarTelaPrestador");
        }

        [HttpPost]
        public ActionResult FiltrarPrestador(PRESTADOR item)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            try
            {
                // Executa a operação
                Int32 idAss = (Int32)Session["IdAssinante"];
                List<PRESTADOR> listaObj = new List<PRESTADOR>();
                Session["FiltroPrestador"] = item;
                Int32 volta = baseApp.ExecuteFilter(item.PRES_NM_NOME, item.PRES_NM_RAZAO_SOCIAL, item.PRES_NR_CNPJ, item.PRES_EM_EMAIL_PRINCIPAL, out listaObj);

                // Verifica retorno
                if (volta == 1)
                {
                    return RedirectToAction("MontarTelaPrestador");
                }

                // Sucesso
                listaMaster = listaObj;
                Session["ListaPrestador"]  = listaObj;
                if ((Int32)Session["VoltaPrestador"] == 2)
                {
                    return RedirectToAction("VerCardsPrestador");
                }
                return RedirectToAction("MontarTelaPrestador");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaPrestador");
            }
        }

        public ActionResult VoltarBasePrestador()
        {
            if ((Int32)Session["VoltaPrestador"] == 2)
            {
                return RedirectToAction("VerCardsPrestador");
            }
            return RedirectToAction("MontarTelaPrestador");
        }

        [HttpGet]
        public ActionResult IncluirPrestador()
        {
            // Verifica se tem usuario logado
            USUARIO_SUGESTAO usuario = new USUARIO_SUGESTAO();
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if ((USUARIO_SUGESTAO)Session["UserCredentials"] != null)
            {
                usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];

                // Verfifica permissão
                if (usuario.PERFIL.PERF_SG_SIGLA == "FUN" || usuario.PERFIL.PERF_SG_SIGLA == "VIS")
                {
                    Session["MensCliente"] = 2;
                    return RedirectToAction("MontarTelaCliente");
                }
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idAss = (Int32)Session["IdAssinante"];

            // Prepara listas

            // Prepara view
            Session["PrestadorNovo"] = 0;
            PRESTADOR item = new PRESTADOR();
            PrestadorViewModel vm = Mapper.Map<PRESTADOR, PrestadorViewModel>(item);
            vm.PRES_DT_DATA_CADASTRO = DateTime.Today;
            vm.PRES_IN_FLAG_ATIVO = 1;
            vm.PRES_IN_POSSUI_SEGURO = 0;
            vm.PRES_IN_PROPRIA_SEGURO = 0;
            vm.PRES_IN_RASTREAMENTO = 0;
            vm.PRES_IN_TERCEIRO_SEGURO = 0;
            vm.PRES_IN_UNIFORME_ECOASSIST = 0;
            vm.PRES_IN_UNIFORME_PROPRIO = 0;
            vm.PRES_NR_FUNCIONARIOS_CLT = 0;
            vm.PRES_NR_FUNCIONARIOS_TERCEIROS = 0;
            vm.PRES_NR_PROPRIA_CAMINHAO = 0;
            vm.PRES_NR_PROPRIA_MOTO = 0;
            vm.PRES_NR_PROPRIA_UTILITARIO = 0;
            vm.PRES_NR_TERCEIRO_CAMINHAO = 0;
            vm.PRES_NR_TERCEIRO_MOTO = 0;
            vm.PRES_NR_TERCEIRO_UTILITARIO = 0;
            return View(vm);
        }

        [HttpPost]
        public ActionResult IncluirPrestador(PrestadorViewModel vm)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR item = Mapper.Map<PrestadorViewModel, PRESTADOR>(vm);
                    USUARIO_SUGESTAO usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    Int32 volta = baseApp.ValidateCreate(item, usuario);

                    // Verifica retorno
                    if (volta == 1)
                    {
                        Session["MensPrestador"] = 3;
                        return RedirectToAction("MontarTelaPrestador", "Prestador");
                    }

                    // Cria pastas
                    String caminho = "/Imagens/1" + "/Prestadores/" + item.PRES_CD_ID.ToString() + "/Anexos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));

                    // Sucesso
                    listaMaster = new List<PRESTADOR>();
                    Session["ListaPrestador"] = null;
                    Session["IncluirPrestador"] = 1;
                    Session["PrestadorNovo"] = item.PRES_CD_ID;
                    Session["Prestadores"] = baseApp.GetAllItens();

                    // Pesquisa CNPJ
                    if (item.PRES_NR_CNPJ != null)
                    {
                        var lstQs = PesquisaCNPJ(item);

                        foreach (var qs in lstQs)
                        {
                            Int32 voltaQs = pcnpjApp.ValidateCreate(qs, usuario);
                        }
                    }

                    Session["IdPrestador"] = item.PRES_CD_ID;
                    Session["IdVolta"] = item.PRES_CD_ID;
                    if (Session["FileQueuePrestador"] != null)
                    {
                        List<FileQueue> fq = (List<FileQueue>)Session["FileQueuePrestador"];

                        foreach (var file in fq)
                        {
                            if (file.Profile == null)
                            {
                                UploadFileQueuePrestador(file);
                            }
                        }
                        Session["FileQueuePrestador"] = null;
                    }
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult EditarPrestador(Int32 id)
        {
            // Verifica se tem usuario logado
            USUARIO_SUGESTAO usuario = new USUARIO_SUGESTAO();
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if ((USUARIO_SUGESTAO)Session["UserCredentials"] != null)
            {
                usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];

                // Verfifica permissão
                if (usuario.PERFIL.PERF_SG_SIGLA == "FUN" || usuario.PERFIL.PERF_SG_SIGLA == "VIS")
                {
                    Session["MensPrestador"] = 2;
                    return RedirectToAction("MontarTelaPrestador");
                }
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idAss = (Int32)Session["IdAssinante"];

            // Prepara view
            PRESTADOR item = baseApp.GetItemById(id);
            ViewBag.QuadroSoci = pcnpjApp.GetByPrestador(item);

            if (Session["MensCliente"] != null)
            {
                // Mensagem
                if ((Int32)Session["MensPrestador"] == 5)
                {
                    ModelState.AddModelError("", SMS_Mensagens.ResourceManager.GetString("M0019", CultureInfo.CurrentCulture));
                }
                if ((Int32)Session["MensPrestador"] == 6)
                {
                    ModelState.AddModelError("", SMS_Mensagens.ResourceManager.GetString("M0024", CultureInfo.CurrentCulture));
                }
            }

            // Indicadores
            ViewBag.Incluir = (Int32)Session["IncluirPrestador"];

            Session["VoltaPrestador"] = 1;
            objetoAntes = item;
            Session["Prestador"] = item;
            Session["IdPrestador"] = id;
            Session["IdVolta"] = id;
            Session["VoltaCEP"] = 1;
            PrestadorViewModel vm = Mapper.Map<PRESTADOR, PrestadorViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult EditarPrestador(PrestadorViewModel vm)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR cli = baseApp.GetItemById(vm.PRES_CD_ID);
            ViewBag.QuadroSoci = pcnpjApp.GetByPrestador(cli);
            ViewBag.Incluir = (Int32)Session["IncluirPrestador"];
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR item = Mapper.Map<PrestadorViewModel, PRESTADOR>(vm);
                    Int32 volta = baseApp.ValidateEdit(item, objetoAntes, usuarioLogado);

                    // Verifica retorno

                    // Sucesso
                    listaMaster = new List<PRESTADOR>();
                    Session["ListaPrestador"] = null;
                    Session["IncluirPrestador"] = 0;

                    if (Session["FiltroPrestador"] != null)
                    {
                        FiltrarPrestador((PRESTADOR)Session["FiltroPrestador"]);
                    }

                    if ((Int32)Session["VoltaPrestador"] == 2)
                    {
                        return RedirectToAction("VerCardsPrestador");
                    }
                    return RedirectToAction("MontarTelaPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    vm = Mapper.Map<PRESTADOR, PrestadorViewModel>(cli);
                    return View(vm);
                }
            }
            else
            {
                vm = Mapper.Map<PRESTADOR, PrestadorViewModel>(cli);
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult ExcluirPrestador(Int32 id)
        {
            // Verifica se tem usuario logado
            USUARIO_SUGESTAO usuario = new USUARIO_SUGESTAO();
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if ((USUARIO_SUGESTAO)Session["UserCredentials"] != null)
            {
                usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];

                // Verfifica permissão
                if (usuario.PERFIL.PERF_SG_SIGLA == "FUN" || usuario.PERFIL.PERF_SG_SIGLA == "VIS")
                {
                    Session["MensPrestador"] = 2;
                    return RedirectToAction("MontarTelaPrestador");
                }
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Prepara view
            PRESTADOR item = baseApp.GetItemById(id);
            PrestadorViewModel vm = Mapper.Map<PRESTADOR, PrestadorViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ExcluirPrestador(PrestadorViewModel vm)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            try
            {
                // Executa a operação
                USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                PRESTADOR item = Mapper.Map<PrestadorViewModel, PRESTADOR>(vm);
                Int32 volta = baseApp.ValidateDelete(item, usuarioLogado);

                // Verifica retorno
                if (volta == 1)
                {
                    Session["MensPrestador"] = 4;
                    return RedirectToAction("MontarTelaPrestador", "Prestador");
                }

                // Sucesso
                listaMaster = new List<PRESTADOR>();
                Session["ListaPrestador"] = null;
                return RedirectToAction("MontarTelaPrestador");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult ReativarPrestador(Int32 id)
        {
            // Verifica se tem usuario logado
            USUARIO_SUGESTAO usuario = new USUARIO_SUGESTAO();
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if ((USUARIO_SUGESTAO)Session["UserCredentials"] != null)
            {
                usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];

                // Verfifica permissão
                if (usuario.PERFIL.PERF_SG_SIGLA == "FUN" || usuario.PERFIL.PERF_SG_SIGLA == "VIS")
                {
                    Session["MensPrestador"] = 2;
                    return RedirectToAction("MontarTelaPrestador");
                }
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Prepara view
            PRESTADOR item = baseApp.GetItemById(id);
            PrestadorViewModel vm = Mapper.Map<PRESTADOR, PrestadorViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ReativarPrestador(PrestadorViewModel vm)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            try
            {
                // Executa a operação
                USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                PRESTADOR item = Mapper.Map<PrestadorViewModel, PRESTADOR>(vm);
                Int32 volta = baseApp.ValidateReativar(item, usuarioLogado);

                // Verifica retorno

                // Sucesso
                listaMaster = new List<PRESTADOR>();
                Session["ListaPrestador"] = null;
                return RedirectToAction("MontarTelaPrestador");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        public ActionResult VerCardsPrestador()
        {
            // Verifica se tem usuario logado
            USUARIO_SUGESTAO usuario = new USUARIO_SUGESTAO();
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if ((USUARIO_SUGESTAO)Session["UserCredentials"] != null)
            {
                usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];

                if (usuario.PERFIL.PERF_SG_SIGLA == "VIS")
                {
                    Session["MensPermissao"] = 2;
                    return RedirectToAction("CarregarBase", "BaseAdmin");
                }
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Carrega listas
            if ((List<PRESTADOR>)Session["ListaPrestador"] == null || ((List<PRESTADOR>)Session["ListaPrestador"]).Count == 0)
            {
                listaMaster = baseApp.GetAllItens();
                Session["ListaPrestador"] = listaMaster;
            }

            ViewBag.Listas = (List<PRESTADOR>)Session["ListaPrestador"];
            ViewBag.Title = "Prestador";
            Session["Prestador"] = null;

            // Indicadores
            ViewBag.Prestadores = ((List<PRESTADOR>)Session["ListaPrestador"]).Count;
            ViewBag.Perfil = usuario.PERFIL.PERF_SG_SIGLA;

            if (Session["MensPrestador"] != null)
            {
            }

            // Abre view
            Session["VoltaPrestador"] = 2;
            objeto = new PRESTADOR();
            return View(objeto);
        }

        [HttpGet]
        public ActionResult VerAnexoPrestador(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            // Prepara view
            PRESTADOR_ANEXO item = baseApp.GetAnexoById(id);
            return View(item);
        }

        public ActionResult VoltarAnexoPrestador()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            return RedirectToAction("EditarPrestador", new { id = (Int32)Session["IdPrestador"] });
        }

        public ActionResult VoltarAjudante()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            return RedirectToAction("EditarAjudante", new { id = (Int32)Session["IdAjudante"] });
        }

        public ActionResult VoltarMotorista()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            return RedirectToAction("EditarMotorista", new { id = (Int32)Session["IdMotorista"] });
        }

        public ActionResult VoltarVeiculo()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            return RedirectToAction("EditarVeiculo", new { id = (Int32)Session["IdVeiculo"] });
        }

        public FileResult DownloadPrestador(Int32 id)
        {
            PRESTADOR_ANEXO item = baseApp.GetAnexoById(id);
            String arquivo = item.PRAN_AQ_ARQUIVO;
            Int32 pos = arquivo.LastIndexOf("/") + 1;
            String nomeDownload = arquivo.Substring(pos);
            String contentType = string.Empty;
            if (arquivo.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }
            else if (arquivo.Contains(".jpg"))
            {
                contentType = "image/jpg";
            }
            else if (arquivo.Contains(".png"))
            {
                contentType = "image/png";
            }
            return File(arquivo, contentType, nomeDownload);
        }

        [HttpGet]
        public ActionResult IncluirComentario()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 id = (Int32)Session["IdVolta"];
            PRESTADOR item = baseApp.GetItemById(id);
            USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
            PRESTADOR_ANOTACOES coment = new PRESTADOR_ANOTACOES();
            PrestadorAnotacoesViewModel vm = Mapper.Map<PRESTADOR_ANOTACOES, PrestadorAnotacoesViewModel>(coment);
            vm.PRAN_DT_ANOTACAO = DateTime.Now;
            vm.PRES_CD_ID = item.PRES_CD_ID;
            vm.USUARIO_SUGESTAO = usuarioLogado;
            vm.USUA_CD_ID = usuarioLogado.USUA_CD_ID;
            return View(vm);
        }

        [HttpPost]
        public ActionResult IncluirComentario(PrestadorAnotacoesViewModel vm)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdPrestador"];
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_ANOTACOES item = Mapper.Map<PrestadorAnotacoesViewModel, PRESTADOR_ANOTACOES>(vm);
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR not = baseApp.GetItemById(idNot);

                    item.USUARIO_SUGESTAO = null;
                    not.PRESTADOR_ANOTACOES.Add(item);
                    objetoAntes = not;
                    Int32 volta = baseApp.ValidateEdit(not, objetoAntes);

                    // Verifica retorno

                    // Sucesso
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult IncluirAnotacaoAjudante()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 id = (Int32)Session["IdAjudante"];
            PRESTADOR_AJUDANTE item = baseApp.GetAjudanteById(id);
            USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
            PRESTADOR_AJUDANTE_ANOTACOES coment = new PRESTADOR_AJUDANTE_ANOTACOES();
            PrestadorAjudanteAnotacoesViewModel vm = Mapper.Map<PRESTADOR_AJUDANTE_ANOTACOES, PrestadorAjudanteAnotacoesViewModel>(coment);
            vm.PRAA_DT_ANOTACAO = DateTime.Now;
            vm.PRAJ_CD_ID = item.PRAJ_CD_ID;
            vm.USUARIO_SUGESTAO = usuarioLogado;
            vm.USUA_CD_ID = usuarioLogado.USUA_CD_ID;
            return View(vm);
        }

        [HttpPost]
        public ActionResult IncluirAnotacaoAjudante(PrestadorAjudanteAnotacoesViewModel vm)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdAjudante"];
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_AJUDANTE_ANOTACOES item = Mapper.Map<PrestadorAjudanteAnotacoesViewModel, PRESTADOR_AJUDANTE_ANOTACOES>(vm);
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR_AJUDANTE not = baseApp.GetAjudanteById(idNot);

                    item.USUARIO_SUGESTAO = null;
                    not.PRESTADOR_AJUDANTE_ANOTACOES.Add(item);
                    Int32 volta = baseApp.ValidateEditAjudante(not);

                    // Verifica retorno

                    // Sucesso
                    return RedirectToAction("VoltarAjudante");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult IncluirAnotacaoMotorista()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 id = (Int32)Session["IdMotorista"];
            PRESTADOR_MOTORISTA item = baseApp.GetMotoristaById(id);
            USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
            PRESTADOR_MOTORISTA_ANOTACOES coment = new PRESTADOR_MOTORISTA_ANOTACOES();
            PrestadorMotoristaAnotacoesViewModel vm = Mapper.Map<PRESTADOR_MOTORISTA_ANOTACOES, PrestadorMotoristaAnotacoesViewModel>(coment);
            vm.PRMA_DT_ANOTACAO = DateTime.Now;
            vm.PRMO_CD_ID = item.PRMO_CD_ID;
            vm.USUARIO_SUGESTAO = usuarioLogado;
            vm.USUA_CD_ID = usuarioLogado.USUA_CD_ID;
            return View(vm);
        }

        [HttpPost]
        public ActionResult IncluirAnotacaoMotorista(PrestadorMotoristaAnotacoesViewModel vm)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdMotorista"];
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_MOTORISTA_ANOTACOES item = Mapper.Map<PrestadorMotoristaAnotacoesViewModel, PRESTADOR_MOTORISTA_ANOTACOES>(vm);
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR_MOTORISTA not = baseApp.GetMotoristaById(idNot);

                    item.USUARIO_SUGESTAO = null;
                    not.PRESTADOR_MOTORISTA_ANOTACOES.Add(item);
                    Int32 volta = baseApp.ValidateEditMotorista(not);

                    // Verifica retorno

                    // Sucesso
                    return RedirectToAction("VoltarMotorista");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult IncluirAnotacaoVeiculo()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 id = (Int32)Session["IdVeiculo"];
            PRESTADOR_VEICULO item = baseApp.GetVeiculoById(id);
            USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
            PRESTADOR_VEICULO_ANOTACOES coment = new PRESTADOR_VEICULO_ANOTACOES();
            PrestadorVeiculoAnotacoesViewModel vm = Mapper.Map<PRESTADOR_VEICULO_ANOTACOES, PrestadorVeiculoAnotacoesViewModel>(coment);
            vm.PRVA_DT_ANOTACAO = DateTime.Now;
            vm.PRVE_CD_ID = item.PRVE_CD_ID;
            vm.USUARIO_SUGESTAO = usuarioLogado;
            vm.USUA_CD_ID = usuarioLogado.USUA_CD_ID;
            return View(vm);
        }

        [HttpPost]
        public ActionResult IncluirAnotacaoVeiculo(PrestadorVeiculoAnotacoesViewModel vm)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdVeiculo"];
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_VEICULO_ANOTACOES item = Mapper.Map<PrestadorVeiculoAnotacoesViewModel, PRESTADOR_VEICULO_ANOTACOES>(vm);
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR_VEICULO not = baseApp.GetVeiculoById(idNot);

                    item.USUARIO_SUGESTAO = null;
                    not.PRESTADOR_VEICULO_ANOTACOES.Add(item);
                    Int32 volta = baseApp.ValidateEditVeiculo(not);

                    // Verifica retorno

                    // Sucesso
                    return RedirectToAction("VoltarVeiculo");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpPost]
        public void UploadFileToSession(IEnumerable<HttpPostedFileBase> files, String profile)
        {
            List<FileQueue> queue = new List<FileQueue>();

            foreach (var file in files)
            {
                FileQueue f = new FileQueue();
                f.Name = Path.GetFileName(file.FileName);
                f.ContentType = Path.GetExtension(file.FileName);

                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                f.Contents = ms.ToArray();

                if (profile != null)
                {
                    if (file.FileName.Equals(profile))
                    {
                        f.Profile = 1;
                    }
                }

                queue.Add(f);
            }
            Session["FileQueuePrestador"] = queue;
        }

        [HttpPost]
        public void UploadFileToSessionAjudante(IEnumerable<HttpPostedFileBase> files, String profile)
        {
            List<FileQueue> queue = new List<FileQueue>();

            foreach (var file in files)
            {
                FileQueue f = new FileQueue();
                f.Name = Path.GetFileName(file.FileName);
                f.ContentType = Path.GetExtension(file.FileName);

                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                f.Contents = ms.ToArray();

                if (profile != null)
                {
                    if (file.FileName.Equals(profile))
                    {
                        f.Profile = 1;
                    }
                }

                queue.Add(f);
            }
            Session["FileQueueAjudante"] = queue;
        }

        [HttpPost]
        public void UploadFileToSessionMotorista(IEnumerable<HttpPostedFileBase> files, String profile)
        {
            List<FileQueue> queue = new List<FileQueue>();

            foreach (var file in files)
            {
                FileQueue f = new FileQueue();
                f.Name = Path.GetFileName(file.FileName);
                f.ContentType = Path.GetExtension(file.FileName);

                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                f.Contents = ms.ToArray();

                if (profile != null)
                {
                    if (file.FileName.Equals(profile))
                    {
                        f.Profile = 1;
                    }
                }

                queue.Add(f);
            }
            Session["FileQueueMotorista"] = queue;
        }

        [HttpPost]
        public void UploadFileToSessionVeiculo(IEnumerable<HttpPostedFileBase> files, String profile)
        {
            List<FileQueue> queue = new List<FileQueue>();

            foreach (var file in files)
            {
                FileQueue f = new FileQueue();
                f.Name = Path.GetFileName(file.FileName);
                f.ContentType = Path.GetExtension(file.FileName);

                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                f.Contents = ms.ToArray();

                if (profile != null)
                {
                    if (file.FileName.Equals(profile))
                    {
                        f.Profile = 1;
                    }
                }

                queue.Add(f);
            }
            Session["FileQueueVeiculo"] = queue;
        }

        [HttpPost]
        public ActionResult UploadFileQueuePrestador(FileQueue file)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdPrestador"];

            if (file == null)
            {
                Session["MensPrestador"] = 5;
                return RedirectToAction("VoltarAnexoPrestador");
            }

            PRESTADOR item = baseApp.GetById(idNot);
            USUARIO_SUGESTAO usu = (USUARIO_SUGESTAO)Session["UserCredentials"];
            var fileName = file.Name;
            if (fileName.Length > 50)
            {
                Session["MensPrestador"] = 6;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            String caminho = "/Imagens/1" + "/Prestadores/" + item.PRES_CD_ID.ToString() + "/Anexos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            System.IO.File.WriteAllBytes(path, file.Contents);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            PRESTADOR_ANEXO foto = new PRESTADOR_ANEXO();
            foto.PRAN_AQ_ARQUIVO = "~" + caminho + fileName;
            foto.PRAN_DT_ANEXO = DateTime.Today;
            foto.PRAN_IN_ATIVO = 1;
            Int32 tipo = 3;
            if (extensao.ToUpper() == ".JPG" || extensao.ToUpper() == ".GIF" || extensao.ToUpper() == ".PNG" || extensao.ToUpper() == ".JPEG")
            {
                tipo = 1;
            }
            if (extensao.ToUpper() == ".MP4" || extensao.ToUpper() == ".AVI" || extensao.ToUpper() == ".MPEG")
            {
                tipo = 2;
            }
            if (extensao.ToUpper() == ".PDF")
            {
                tipo = 3;
            }
            foto.PRAN_IN_TIPO = tipo;
            foto.PRAN_NM_TITULO = fileName;
            foto.PRES_CD_ID = item.PRES_CD_ID;

            item.PRESTADOR_ANEXO.Add(foto);
            objetoAntes = item;
            Int32 volta = baseApp.ValidateEdit(item, objetoAntes);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpPost]
        public ActionResult UploadFilePrestador(HttpPostedFileBase file)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdPrestador"];

            if (file == null)
            {
                Session["MensPrestador"] = 5;
                return RedirectToAction("VoltarAnexoPrestador");
            }

            PRESTADOR item = baseApp.GetById(idNot);
            USUARIO_SUGESTAO usu = (USUARIO_SUGESTAO)Session["UserCredentials"];
            var fileName = Path.GetFileName(file.FileName);
            if (fileName.Length > 250)
            {
                Session["MensPrestador"] = 6;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            String caminho = "/Imagens/1" + "/Prestadores/" + item.PRES_CD_ID.ToString() + "/Anexos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            PRESTADOR_ANEXO foto = new PRESTADOR_ANEXO();
            foto.PRAN_AQ_ARQUIVO = "~" + caminho + fileName;
            foto.PRAN_DT_ANEXO = DateTime.Today;
            foto.PRAN_IN_ATIVO = 1;
            Int32 tipo = 3;
            if (extensao.ToUpper() == ".JPG" || extensao.ToUpper() == ".GIF" || extensao.ToUpper() == ".PNG" || extensao.ToUpper() == ".JPEG")
            {
                tipo = 1;
            }
            if (extensao.ToUpper() == ".MP4" || extensao.ToUpper() == ".AVI" || extensao.ToUpper() == ".MPEG")
            {
                tipo = 2;
            }
            if (extensao.ToUpper() == ".PDF")
            {
                tipo = 3;
            }
            foto.PRAN_IN_TIPO = tipo;
            foto.PRAN_NM_TITULO = fileName;
            foto.PRES_CD_ID = item.PRES_CD_ID;

            item.PRESTADOR_ANEXO.Add(foto);
            objetoAntes = item;
            Int32 volta = baseApp.ValidateEdit(item, objetoAntes);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpPost]
        public ActionResult UploadFotoQueueAjudante(FileQueue file)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdAjudante"];
            Int32 idAss = (Int32)Session["IdAssinante"];

            if (file == null)
            {
                Session["MensPrestador"] = 5;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            PRESTADOR_AJUDANTE item = baseApp.GetAjudanteById(idNot);
            USUARIO_SUGESTAO usu = (USUARIO_SUGESTAO)Session["UserCredentials"];
            var fileName = file.Name;
            if (fileName.Length > 250)
            {
                Session["MensPrestador"] = 6;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            String caminho = "/Imagens/1" + "/Ajudantes/" + item.PRAJ_CD_ID.ToString() + "/Fotos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            System.IO.File.WriteAllBytes(path, file.Contents);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            item.PARJ__AQ_FOTO = "~" + caminho + fileName;
            Int32 volta = baseApp.ValidateEditAjudante(item);
            return RedirectToAction("VoltarAnexoPresyador");
        }

        [HttpPost]
        public ActionResult UploadFotoAjudante(HttpPostedFileBase file)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdAjudante"];
            Int32 idAss = (Int32)Session["IdAssinante"];

            if (file == null)
            {
                Session["MensPrestador"] = 5;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            PRESTADOR_AJUDANTE item = baseApp.GetAjudanteById(idNot);
            USUARIO_SUGESTAO usu = (USUARIO_SUGESTAO)Session["UserCredentials"];
            var fileName = Path.GetFileName(file.FileName);
            if (fileName.Length > 250)
            {
                Session["MensPrestador"] = 6;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            String caminho = "/Imagens/1" + "/Ajudantes/" + item.PRAJ_CD_ID.ToString() + "/Fotos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            item.PARJ__AQ_FOTO = "~" + caminho + fileName;
            Int32 volta = baseApp.ValidateEditAjudante(item);
            return RedirectToAction("VoltarAjudante");
        }

        [HttpPost]
        public ActionResult UploadFotoQueueMotorista(FileQueue file)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdMotorista"];
            Int32 idAss = (Int32)Session["IdAssinante"];

            if (file == null)
            {
                Session["MensPrestador"] = 5;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            PRESTADOR_MOTORISTA item = baseApp.GetMotoristaById(idNot);
            USUARIO_SUGESTAO usu = (USUARIO_SUGESTAO)Session["UserCredentials"];
            var fileName = file.Name;
            if (fileName.Length > 250)
            {
                Session["MensPrestador"] = 6;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            String caminho = "/Imagens/1" + "/Motoristas/" + item.PRMO_CD_ID.ToString() + "/Fotos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            System.IO.File.WriteAllBytes(path, file.Contents);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            item.PRMO_AQ_FOTO = "~" + caminho + fileName;
            Int32 volta = baseApp.ValidateEditMotorista(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpPost]
        public ActionResult UploadFotoMotorista(HttpPostedFileBase file)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdMotorista"];
            Int32 idAss = (Int32)Session["IdAssinante"];

            if (file == null)
            {
                Session["MensPrestador"] = 5;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            PRESTADOR_MOTORISTA item = baseApp.GetMotoristaById(idNot);
            USUARIO_SUGESTAO usu = (USUARIO_SUGESTAO)Session["UserCredentials"];
            var fileName = Path.GetFileName(file.FileName);
            if (fileName.Length > 250)
            {
                Session["MensPrestador"] = 6;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            String caminho = "/Imagens/1" + "/Motoristas/" + item.PRMO_CD_ID.ToString() + "/Fotos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            item.PRMO_AQ_FOTO = "~" + caminho + fileName;
            Int32 volta = baseApp.ValidateEditMotorista(item);
            return RedirectToAction("VoltarMotorista");
        }

        [HttpPost]
        public ActionResult UploadFotoQueueVeiculo(FileQueue file)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdVeiculo"];
            Int32 idAss = (Int32)Session["IdAssinante"];

            if (file == null)
            {
                Session["MensPrestador"] = 5;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            PRESTADOR_VEICULO item = baseApp.GetVeiculoById(idNot);
            USUARIO_SUGESTAO usu = (USUARIO_SUGESTAO)Session["UserCredentials"];
            var fileName = file.Name;
            if (fileName.Length > 250)
            {
                Session["MensPrestador"] = 6;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            String caminho = "/Imagens/1" + "/Veiculos/" + item.PRVE_CD_ID.ToString() + "/Fotos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            System.IO.File.WriteAllBytes(path, file.Contents);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            item.PRVE_AQ_FOTO = "~" + caminho + fileName;
            Int32 volta = baseApp.ValidateEditVeiculo(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpPost]
        public ActionResult UploadFotoVeiculo(HttpPostedFileBase file)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            Int32 idNot = (Int32)Session["IdVeiculo"];
            Int32 idAss = (Int32)Session["IdAssinante"];

            if (file == null)
            {
                Session["MensPrestador"] = 5;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            PRESTADOR_VEICULO item = baseApp.GetVeiculoById(idNot);
            USUARIO_SUGESTAO usu = (USUARIO_SUGESTAO)Session["UserCredentials"];
            var fileName = Path.GetFileName(file.FileName);
            if (fileName.Length > 250)
            {
                Session["MensPrestador"] = 6;
                return RedirectToAction("VoltarAnexoPrestador");
            }
            String caminho = "/Imagens/1" + "/Veiculos/" + item.PRVE_CD_ID.ToString() + "/Fotos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            item.PRVE_AQ_FOTO = "~" + caminho + fileName;
            Int32 volta = baseApp.ValidateEditVeiculo(item);
            return RedirectToAction("VoltarVeiculo");
        }

        [HttpPost]
        public JsonResult PesquisaCEP_Javascript(String cep, int tipoEnd)
        {
            // Chama servico ECT
            ZipCodeLoad zipLoad = new ZipCodeLoad();
            ZipCodeInfo end = new ZipCodeInfo();
            ZipCode zipCode = null;
            cep = CrossCutting.ValidarNumerosDocumentos.RemoveNaoNumericos(cep);
            if (ZipCode.TryParse(cep, out zipCode))
            {
                end = zipLoad.Find(zipCode);
            }

            // Atualiza
            var hash = new Hashtable();
            hash.Add("PREN_NM_ENDERECO", end.Address);
            hash.Add("PREN_NR_NUMERO", end.Complement);
            hash.Add("PREN_NM_BAIRRO", end.District);
            hash.Add("PREN_NM_CIDADE", end.City);
            hash.Add("UF_CD_ID", baseApp.GetUFbySigla(end.Uf).UF_CD_ID);
            hash.Add("PREN_NR_CEP", cep);

            // Retorna
            Session["VoltaCEP"] = 2;
            return Json(hash);
        }

        [HttpGet]
        public ActionResult EditarContato(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            // Prepara view
            PRESTADOR_CONTATO item = baseApp.GetContatoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            PrestadorContatoViewModel vm = Mapper.Map<PRESTADOR_CONTATO, PrestadorContatoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarContato(PrestadorContatoViewModel vm)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR_CONTATO item = Mapper.Map<PrestadorContatoViewModel, PRESTADOR_CONTATO>(vm);
                    Int32 volta = baseApp.ValidateEditContato(item);

                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult ExcluirContato(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_CONTATO item = baseApp.GetContatoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRCO_IN_ATIVO = 0;
            Int32 volta = baseApp.ValidateEditContato(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult ReativarContato(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_CONTATO item = baseApp.GetContatoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRCO_IN_ATIVO = 1;
            Int32 volta = baseApp.ValidateEditContato(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult IncluirContato()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Prepara view
            PRESTADOR_CONTATO item = new PRESTADOR_CONTATO();
            PrestadorContatoViewModel vm = Mapper.Map<PRESTADOR_CONTATO, PrestadorContatoViewModel>(item);
            vm.PRES_CD_ID = (Int32)Session["IdPrestador"];
            vm.PRCO_IN_ATIVO = 1;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirContato(PrestadorContatoViewModel vm)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_CONTATO item = Mapper.Map<PrestadorContatoViewModel, PRESTADOR_CONTATO>(vm);
                    Int32 volta = baseApp.ValidateCreateContato(item);
                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult EditarAjudante(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            // Prepara view
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");

            PRESTADOR_AJUDANTE item = baseApp.GetAjudanteById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            PrestadorAjudanteViewModel vm = Mapper.Map<PRESTADOR_AJUDANTE, PrestadorAjudanteViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarAjudante(PrestadorAjudanteViewModel vm)
        {
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR_AJUDANTE item = Mapper.Map<PrestadorAjudanteViewModel, PRESTADOR_AJUDANTE>(vm);
                    Int32 volta = baseApp.ValidateEditAjudante(item);

                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult ExcluirAjudante(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_AJUDANTE item = baseApp.GetAjudanteById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRAJ_IN_ATIVO = 0;
            Int32 volta = baseApp.ValidateEditAjudante(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult ReativarAjudante(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_AJUDANTE item = baseApp.GetAjudanteById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRAJ_IN_ATIVO = 1;
            Int32 volta = baseApp.ValidateEditAjudante(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult IncluirAjudante()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Prepara view
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");

            PRESTADOR_AJUDANTE item = new PRESTADOR_AJUDANTE();
            PrestadorAjudanteViewModel vm = Mapper.Map<PRESTADOR_AJUDANTE, PrestadorAjudanteViewModel>(item);
            vm.PRES_CD_ID = (Int32)Session["IdPrestador"];
            vm.PRAJ_IN_ATIVO = 1;
            vm.PRAJ_DT_CADASTRO = DateTime.Today.Date;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirAjudante(PrestadorAjudanteViewModel vm)
        {
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");

            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_AJUDANTE item = Mapper.Map<PrestadorAjudanteViewModel, PRESTADOR_AJUDANTE>(vm);
                    Int32 volta = baseApp.ValidateCreateAjudante(item);

                    // Cria pastas
                    String caminho = "/Imagens/1" + "/Ajudantes/" + item.PRAJ_CD_ID.ToString() + "/Fotos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));

                    Session["IdPrestador"] = item.PRES_CD_ID;
                    Session["IdAjudante"] = item.PRAJ_CD_ID;
                    if (Session["FileQueueAjudante"] != null)
                    {
                        List<FileQueue> fq = (List<FileQueue>)Session["FileQueueAjudante"];

                        foreach (var file in fq)
                        {
                            if (file.Profile == null)
                            {
                                UploadFotoQueueAjudante(file);
                            }
                        }
                        Session["FileQueueAjudante"] = null;
                    }

                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult EditarBanco(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            // Prepara view
            ViewBag.Bancos = new SelectList(baseApp.GetAllBanco(), "BANC_CD_ID", "BANC_NM_NOME");
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipoContas(), "TICO_CD_ID", "TICO_NM_NOME");


            PRESTADOR_BANCO item = baseApp.GetBancoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            PrestadorBancoViewModel vm = Mapper.Map<PRESTADOR_BANCO, PrestadorBancoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarBanco(PrestadorBancoViewModel vm)
        {
            ViewBag.Bancos = new SelectList(baseApp.GetAllBanco(), "BANC_CD_ID", "BANC_NM_NOME");
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipoContas(), "TICO_CD_ID", "TICO_NM_NOME");
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR_BANCO item = Mapper.Map<PrestadorBancoViewModel, PRESTADOR_BANCO>(vm);
                    Int32 volta = baseApp.ValidateEditBanco(item);

                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult ExcluirBanco(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_BANCO item = baseApp.GetBancoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRBA_IN_ATIVO = 0;
            Int32 volta = baseApp.ValidateEditBanco(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult ReativarBanco(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_BANCO item = baseApp.GetBancoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRBA_IN_ATIVO = 1;
            Int32 volta = baseApp.ValidateEditBanco(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult IncluirBanco()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Prepara view
            ViewBag.Bancos = new SelectList(baseApp.GetAllBanco(), "BANC_CD_ID", "BANC_NM_NOME");
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipoContas(), "TICO_CD_ID", "TICO_NM_NOME");

            PRESTADOR_BANCO item = new PRESTADOR_BANCO();
            PrestadorBancoViewModel vm = Mapper.Map<PRESTADOR_BANCO, PrestadorBancoViewModel>(item);
            vm.PRES_CD_ID = (Int32)Session["IdPrestador"];
            vm.PRBA_IN_ATIVO = 1;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirBanco(PrestadorBancoViewModel vm)
        {
            ViewBag.Bancos = new SelectList(baseApp.GetAllBanco(), "BANC_CD_ID", "BANC_NM_NOME");
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipoContas(), "TICO_CD_ID", "TICO_NM_NOME");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_BANCO item = Mapper.Map<PrestadorBancoViewModel, PRESTADOR_BANCO>(vm);
                    Int32 volta = baseApp.ValidateCreateBanco(item);
                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult EditarCertificado(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            // Prepara view
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipoCertificados(), "TICE_CD_ID", "TICE_NM_NOME");

            PRESTADOR_CERTIFICADO item = baseApp.GetCertificadoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            PrestadorCertificadoViewModel vm = Mapper.Map<PRESTADOR_CERTIFICADO, PrestadorCertificadoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCertificado(PrestadorCertificadoViewModel vm)
        {
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipoCertificados(), "TICE_CD_ID", "TICE_NM_NOME");
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR_CERTIFICADO item = Mapper.Map<PrestadorCertificadoViewModel, PRESTADOR_CERTIFICADO>(vm);
                    Int32 volta = baseApp.ValidateEditCertificado(item);

                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult ExcluirCertificado(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_CERTIFICADO item = baseApp.GetCertificadoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRCE_IN_ATIVO = 0;
            Int32 volta = baseApp.ValidateEditCertificado(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult ReativarCertificado(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_CERTIFICADO item = baseApp.GetCertificadoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRCE_IN_ATIVO = 1;
            Int32 volta = baseApp.ValidateEditCertificado(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult IncluirCertificado()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Prepara view
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipoCertificados(), "TICE_CD_ID", "TICE_NM_NOME");

            PRESTADOR_CERTIFICADO item = new PRESTADOR_CERTIFICADO();
            PrestadorCertificadoViewModel vm = Mapper.Map<PRESTADOR_CERTIFICADO, PrestadorCertificadoViewModel>(item);
            vm.PRES_CD_ID = (Int32)Session["IdPrestador"];
            vm.PRCE_IN_ATIVO = 1;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirCertificado(PrestadorCertificadoViewModel vm)
        {
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipoCertificados(), "TICE_CD_ID", "TICE_NM_NOME");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_CERTIFICADO item = Mapper.Map<PrestadorCertificadoViewModel, PRESTADOR_CERTIFICADO>(vm);
                    Int32 volta = baseApp.ValidateCreateCertificado(item);
                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult EditarEndereco(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            // Prepara view
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");

            PRESTADOR_ENDERECO item = baseApp.GetEnderecoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            PrestadorEnderecoViewModel vm = Mapper.Map<PRESTADOR_ENDERECO, PrestadorEnderecoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarEndereco(PrestadorEnderecoViewModel vm)
        {
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR_ENDERECO item = Mapper.Map<PrestadorEnderecoViewModel, PRESTADOR_ENDERECO>(vm);
                    Int32 volta = baseApp.ValidateEditEndereco(item);

                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult ExcluirEndereco(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_ENDERECO item = baseApp.GetEnderecoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PREN_IN_ATIVO = 0;
            Int32 volta = baseApp.ValidateEditEndereco(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult ReativarEndereco(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_ENDERECO item = baseApp.GetEnderecoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PREN_IN_ATIVO = 1;
            Int32 volta = baseApp.ValidateEditEndereco(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult IncluirEndereco()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Prepara view
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");

            PRESTADOR_ENDERECO item = new PRESTADOR_ENDERECO();
            PrestadorEnderecoViewModel vm = Mapper.Map<PRESTADOR_ENDERECO, PrestadorEnderecoViewModel>(item);
            vm.PRES_CD_ID = (Int32)Session["IdPrestador"];
            vm.PREN_IN_ATIVO = 1;
            vm.PREN_IN_FLAG_ESTOQUE = 0;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirEndereco(PrestadorEnderecoViewModel vm)
        {
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_ENDERECO item = Mapper.Map<PrestadorEnderecoViewModel, PRESTADOR_ENDERECO>(vm);
                    Int32 volta = baseApp.ValidateCreateEndereco(item);
                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult EditarMotorista(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            // Prepara view
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");
            ViewBag.Cat = new SelectList(baseApp.GetAllCatsCNH(), "CNCA_CD_ID", "CNCA_NM_NOME");

            PRESTADOR_MOTORISTA item = baseApp.GetMotoristaById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            PrestadorMotoristaViewModel vm = Mapper.Map<PRESTADOR_MOTORISTA, PrestadorMotoristaViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarMotorista(PrestadorMotoristaViewModel vm)
        {
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");
            ViewBag.Cat = new SelectList(baseApp.GetAllCatsCNH(), "CNCA_CD_ID", "CNCA_NM_NOME");
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR_MOTORISTA item = Mapper.Map<PrestadorMotoristaViewModel, PRESTADOR_MOTORISTA>(vm);
                    Int32 volta = baseApp.ValidateEditMotorista(item);

                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult ExcluirMotorista(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_MOTORISTA item = baseApp.GetMotoristaById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRMO_IN_ATIVO = 0;
            Int32 volta = baseApp.ValidateEditMotorista(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult ReativarMotorista(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_MOTORISTA item = baseApp.GetMotoristaById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRMO_IN_ATIVO = 1;
            Int32 volta = baseApp.ValidateEditMotorista(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult IncluirMotorista()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Prepara view
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");
            ViewBag.Cat = new SelectList(baseApp.GetAllCatsCNH(), "CNCA_CD_ID", "CNCA_NM_NOME");

            PRESTADOR_MOTORISTA item = new PRESTADOR_MOTORISTA();
            PrestadorMotoristaViewModel vm = Mapper.Map<PRESTADOR_MOTORISTA, PrestadorMotoristaViewModel>(item);
            vm.PRES_CD_ID = (Int32)Session["IdPrestador"];
            vm.PRMO_IN_ATIVO = 1;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirMotorista(PrestadorMotoristaViewModel vm)
        {
            ViewBag.UF = new SelectList(baseApp.GetAllUF(), "UF_CD_ID", "UF_NM_NOME");
            ViewBag.Cat = new SelectList(baseApp.GetAllCatsCNH(), "CNCA_CD_ID", "CNCA_NM_NOME");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_MOTORISTA item = Mapper.Map<PrestadorMotoristaViewModel, PRESTADOR_MOTORISTA>(vm);
                    Int32 volta = baseApp.ValidateCreateMotorista(item);

                    // Cria pastas
                    String caminho = "/Imagens/1" + "/Motoristas/" + item.PRMO_CD_ID.ToString() + "/Fotos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));

                    Session["IdPrestador"] = item.PRES_CD_ID;
                    Session["IdMotorista"] = item.PRMO_CD_ID;
                    if (Session["FileQueueMotorista"] != null)
                    {
                        List<FileQueue> fq = (List<FileQueue>)Session["FileQueueMotorista"];

                        foreach (var file in fq)
                        {
                            if (file.Profile == null)
                            {
                                UploadFotoQueueMotorista(file);
                            }
                        }
                        Session["FileQueueMotorista"] = null;
                    }

                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult EditarRegiao(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            // Prepara view
            ViewBag.Regiao = new SelectList(baseApp.GetAllRegiao(), "REGI_CD_ID", "REGI_NM_NOME");
            ViewBag.Cobertura = new SelectList(baseApp.GetAllRegiaoCobertura(), "RECO_CD_ID", "RECO_NM_NOME");
            List<SelectListItem> prior = new List<SelectListItem>();
            prior.Add(new SelectListItem() { Text = "Normal", Value = "1" });
            prior.Add(new SelectListItem() { Text = "Baixa", Value = "2" });
            prior.Add(new SelectListItem() { Text = "Alta", Value = "3" });
            prior.Add(new SelectListItem() { Text = "Urgente", Value = "4" });
            ViewBag.Prioridade = new SelectList(prior, "Value", "Text");

            PRESTADOR_REGIAO item = baseApp.GetRegiaoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            PrestadorRegiaoViewModel vm = Mapper.Map<PRESTADOR_REGIAO, PrestadorRegiaoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarRegiao(PrestadorRegiaoViewModel vm)
        {
            ViewBag.Regiao = new SelectList(baseApp.GetAllRegiao(), "REGI_CD_ID", "REGI_NM_NOME");
            ViewBag.Cobertura = new SelectList(baseApp.GetAllRegiaoCobertura(), "RECO_CD_ID", "RECO_NM_NOME");
            List<SelectListItem> prior = new List<SelectListItem>();
            prior.Add(new SelectListItem() { Text = "Normal", Value = "1" });
            prior.Add(new SelectListItem() { Text = "Baixa", Value = "2" });
            prior.Add(new SelectListItem() { Text = "Alta", Value = "3" });
            prior.Add(new SelectListItem() { Text = "Urgente", Value = "4" });
            ViewBag.Prioridade = new SelectList(prior, "Value", "Text");
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR_REGIAO item = Mapper.Map<PrestadorRegiaoViewModel, PRESTADOR_REGIAO>(vm);
                    Int32 volta = baseApp.ValidateEditRegiao(item);

                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult ExcluirRegiao(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_REGIAO item = baseApp.GetRegiaoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRRE_IN_ATIVO = 0;
            Int32 volta = baseApp.ValidateEditRegiao(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult ReativarRegiao(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_REGIAO item = baseApp.GetRegiaoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRRE_IN_ATIVO = 1;
            Int32 volta = baseApp.ValidateEditRegiao(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult IncluirRegiao()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Prepara view
            ViewBag.Regiao = new SelectList(baseApp.GetAllRegiao(), "REGI_CD_ID", "REGI_NM_NOME");
            ViewBag.Cobertura = new SelectList(baseApp.GetAllRegiaoCobertura(), "RECO_CD_ID", "RECO_NM_NOME");
            List<SelectListItem> prior = new List<SelectListItem>();
            prior.Add(new SelectListItem() { Text = "Normal", Value = "1" });
            prior.Add(new SelectListItem() { Text = "Baixa", Value = "2" });
            prior.Add(new SelectListItem() { Text = "Alta", Value = "3" });
            prior.Add(new SelectListItem() { Text = "Urgente", Value = "4" });
            ViewBag.Prioridade = new SelectList(prior, "Value", "Text");

            PRESTADOR_REGIAO item = new PRESTADOR_REGIAO();
            PrestadorRegiaoViewModel vm = Mapper.Map<PRESTADOR_REGIAO, PrestadorRegiaoViewModel>(item);
            vm.PRES_CD_ID = (Int32)Session["IdPrestador"];
            vm.PRRE_IN_ATIVO = 1;
            vm.PRRE_IN_SEGUNDA = 0;
            vm.PRRE_IN_TERCA = 0;
            vm.PRRE_IN_QUARTA = 0;
            vm.PRRE_IN_QUINTA = 0;
            vm.PRRE_IN_SEXTA = 0;
            vm.PRRE_IN_SABADO = 0;
            vm.PRRE_IN_DOMINGO = 0;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirRegiao(PrestadorRegiaoViewModel vm)
        {
            ViewBag.Regiao = new SelectList(baseApp.GetAllRegiao(), "REGI_CD_ID", "REGI_NM_NOME");
            ViewBag.Cobertura = new SelectList(baseApp.GetAllRegiaoCobertura(), "RECO_CD_ID", "RECO_NM_NOME");
            List<SelectListItem> prior = new List<SelectListItem>();
            prior.Add(new SelectListItem() { Text = "Normal", Value = "1" });
            prior.Add(new SelectListItem() { Text = "Baixa", Value = "2" });
            prior.Add(new SelectListItem() { Text = "Alta", Value = "3" });
            prior.Add(new SelectListItem() { Text = "Urgente", Value = "4" });
            ViewBag.Prioridade = new SelectList(prior, "Value", "Text");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_REGIAO item = Mapper.Map<PrestadorRegiaoViewModel, PRESTADOR_REGIAO>(vm);
                    Int32 volta = baseApp.ValidateCreateRegiao(item);
                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpPost]
        public JsonResult FiltrarCobertura(Int32? id)
        {
            var listaCobFiltrada = baseApp.GetAllRegiaoCobertura();

            // Filtro para caso o placeholder seja selecionado
            if (id != null)
            {
                listaCobFiltrada = listaCobFiltrada.Where(x => x.REGI_CD_ID == id).ToList();
            }
            return Json(listaCobFiltrada.Select(x => new { x.RECO_CD_ID, x.RECO_NM_NOME }));
        }

        [HttpPost]
        public JsonResult FiltrarRegiao(Int32? id)
        {
            var listaFiltrada = baseApp.GetAllRegiao();

            // Filtro para caso o placeholder seja selecionado
            if (id != null)
            {
                listaFiltrada = listaFiltrada.Where(x => x.REGIAO_COBERTURA.Any(s => s.RECO_CD_ID == id)).ToList();
            }

            return Json(listaFiltrada.Select(x => new { x.REGI_CD_ID, x.REGI_NM_NOME }));
        }

        [HttpGet]
        public ActionResult EditarVeiculo(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            // Prepara view
            ViewBag.Tipo = new SelectList(baseApp.GetAllTipoVeiculo(), "TIVE_CD_ID", "TIVE_NM_NOME");
            ViewBag.Marca = new SelectList(baseApp.GetAllMarcaVeiculo(), "MAVE_CD_ID", "MAVE_NM_CIDADE");
            ViewBag.Modelo = new SelectList(baseApp.GetAllModeloVeiculo(), "MOVE_CD_ID", "MOVE_NM_CIDADE");

            PRESTADOR_VEICULO item = baseApp.GetVeiculoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            PrestadorVeiculoViewModel vm = Mapper.Map<PRESTADOR_VEICULO, PrestadorVeiculoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarVeiculo(PrestadorVeiculoViewModel vm)
        {
            ViewBag.Tipo = new SelectList(baseApp.GetAllTipoVeiculo(), "TIVE_CD_ID", "TIVE_NM_NOME");
            ViewBag.Marca = new SelectList(baseApp.GetAllMarcaVeiculo(), "MAVE_CD_ID", "MAVE_NM_CIDADE");
            ViewBag.Modelo = new SelectList(baseApp.GetAllModeloVeiculo(), "MOVE_CD_ID", "MOVE_NM_CIDADE");
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO_SUGESTAO usuarioLogado = (USUARIO_SUGESTAO)Session["UserCredentials"];
                    PRESTADOR_VEICULO item = Mapper.Map<PrestadorVeiculoViewModel, PRESTADOR_VEICULO>(vm);
                    Int32 volta = baseApp.ValidateEditVeiculo(item);

                    // Verifica retorno
                    return RedirectToAction("VoltarAnexoPrestador");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult ExcluirVeiculo(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_VEICULO item = baseApp.GetVeiculoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRVE_IN_ATIVO = 0;
            Int32 volta = baseApp.ValidateEditVeiculo(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult ReativarVeiculo(Int32 id)
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            PRESTADOR_VEICULO item = baseApp.GetVeiculoById(id);
            objetoAntes = (PRESTADOR)Session["Prestador"];
            item.PRVE_IN_ATIVO = 1;
            Int32 volta = baseApp.ValidateEditVeiculo(item);
            return RedirectToAction("VoltarAnexoPrestador");
        }

        [HttpGet]
        public ActionResult IncluirVeiculo()
        {
            if ((String)Session["Ativa"] == null)
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Prepara view
            ViewBag.Tipo = new SelectList(baseApp.GetAllTipoVeiculo(), "TIVE_CD_ID", "TIVE_NM_NOME");
            ViewBag.Marca = new SelectList(baseApp.GetAllMarcaVeiculo(), "MAVE_CD_ID", "MAVE_NM_CIDADE");
            ViewBag.Modelo = new SelectList(baseApp.GetAllModeloVeiculo(), "MOVE_CD_ID", "MOVE_NM_CIDADE");

            PRESTADOR_VEICULO item = new PRESTADOR_VEICULO();
            PrestadorVeiculoViewModel vm = Mapper.Map<PRESTADOR_VEICULO, PrestadorVeiculoViewModel>(item);
            vm.PRES_CD_ID = (Int32)Session["IdPrestador"];
            vm.PRVE_IN_ATIVO = 1;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirVeiculo(PrestadorVeiculoViewModel vm)
        {
            ViewBag.Tipo = new SelectList(baseApp.GetAllTipoVeiculo(), "TIVE_CD_ID", "TIVE_NM_NOME");
            ViewBag.Marca = new SelectList(baseApp.GetAllMarcaVeiculo(), "MAVE_CD_ID", "MAVE_NM_CIDADE");
            ViewBag.Modelo = new SelectList(baseApp.GetAllModeloVeiculo(), "MOVE_CD_ID", "MOVE_NM_CIDADE");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRESTADOR_VEICULO item = Mapper.Map<PrestadorVeiculoViewModel, PRESTADOR_VEICULO>(vm);
                    Int32 volta = baseApp.ValidateCreateVeiculo(item);

                    // Cria pastas
                    String caminho = "/Imagens/1" + "/Veiculos/" + item.PRVE_CD_ID.ToString() + "/Fotos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));

                    Session["IdPrestador"] = item.PRES_CD_ID;
                    Session["IdVeiculo"] = item.PRVE_CD_ID;
                    if (Session["FileQueueVeiculo"] != null)
                    {
                        List<FileQueue> fq = (List<FileQueue>)Session["FileQueueVeiculo"];

                        foreach (var file in fq)
                        {
                            if (file.Profile == null)
                            {
                                UploadFotoQueueVeiculo(file);
                            }
                        }
                        Session["FileQueueVeiculo"] = null;
                    }

                    // Verifica retorno
                    Session["IdVeiculo"] = item.PRVE_CD_ID;
                    return RedirectToAction("VoltarVeiculo");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        //public ActionResult GerarRelatorioLista()
        //{
        //    if ((String)Session["Ativa"] == null)
        //    {
        //        return RedirectToAction("Login", "ControleAcesso");
        //    }
        //    // Prepara geração
        //    String data = DateTime.Today.Date.ToShortDateString();
        //    data = data.Substring(0, 2) + data.Substring(3, 2) + data.Substring(6, 4);
        //    String nomeRel = "ClienteLista" + "_" + data + ".pdf";
        //    List<PRESTADOR> lista = (List<PRESTADOR>)Session["ListaPrestador"];
        //    PRESTADOR filtro = (PRESTADOR)Session["FiltroPrestador"];
        //    Font meuFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        //    Font meuFont1 = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        //    Font meuFont2 = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //    // Cria documento
        //    Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
        //    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //    pdfDoc.Open();

        //    // Linha horizontal
        //    Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLUE, Element.ALIGN_LEFT, 1)));
        //    pdfDoc.Add(line);

        //    // Cabeçalho
        //    PdfPTable table = new PdfPTable(5);
        //    table.WidthPercentage = 100;
        //    table.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    table.SpacingBefore = 1f;
        //    table.SpacingAfter = 1f;

        //    PdfPCell cell = new PdfPCell();
        //    cell.Border = 0;
        //    Image image = Image.GetInstance(Server.MapPath("~/Images/favicon_SystemBR.jpg"));
        //    image.ScaleAbsolute(50, 50);
        //    cell.AddElement(image);
        //    table.AddCell(cell);

        //    cell = new PdfPCell(new Paragraph("Prestadores - Listagem", meuFont2))
        //    {
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        HorizontalAlignment = Element.ALIGN_CENTER
        //    };
        //    cell.Border = 0;
        //    cell.Colspan = 4;
        //    table.AddCell(cell);
        //    pdfDoc.Add(table);

        //    // Linha Horizontal
        //    Paragraph line1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLUE, Element.ALIGN_LEFT, 1)));
        //    pdfDoc.Add(line1);
        //    line1 = new Paragraph("  ");
        //    pdfDoc.Add(line1);

        //    // Grid
        //    table = new PdfPTable(new float[] { 70f, 150f, 60f, 60f, 150f, 50f, 50f, 20f });
        //    table.WidthPercentage = 100;
        //    table.HorizontalAlignment = 0;
        //    table.SpacingBefore = 1f;
        //    table.SpacingAfter = 1f;

        //    cell = new PdfPCell(new Paragraph("Prestadores selecionados pelos parametros de filtro abaixo", meuFont1))
        //    {
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        HorizontalAlignment = Element.ALIGN_LEFT
        //    };
        //    cell.Colspan = 8;
        //    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    table.AddCell(cell);

        //    cell = new PdfPCell(new Paragraph("Categoria", meuFont))
        //    {
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        HorizontalAlignment = Element.ALIGN_LEFT
        //    };
        //    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    table.AddCell(cell);
        //    cell = new PdfPCell(new Paragraph("Nome", meuFont))
        //    {
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        HorizontalAlignment = Element.ALIGN_LEFT
        //    };
        //    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    table.AddCell(cell);
        //    cell = new PdfPCell(new Paragraph("CPF", meuFont))
        //    {
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        HorizontalAlignment = Element.ALIGN_LEFT
        //    };
        //    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    table.AddCell(cell);
        //    cell = new PdfPCell(new Paragraph("CNPJ", meuFont))
        //    {
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        HorizontalAlignment = Element.ALIGN_LEFT
        //    };
        //    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    table.AddCell(cell);
        //    cell = new PdfPCell(new Paragraph("E-Mail", meuFont))
        //    {
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        HorizontalAlignment = Element.ALIGN_LEFT
        //    };
        //    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    table.AddCell(cell);
        //    cell = new PdfPCell(new Paragraph("Telefone", meuFont))
        //    {
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        HorizontalAlignment = Element.ALIGN_LEFT
        //    };
        //    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    table.AddCell(cell);
        //    cell = new PdfPCell(new Paragraph("Cidade", meuFont))
        //    {
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        HorizontalAlignment = Element.ALIGN_LEFT
        //    };
        //    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    table.AddCell(cell);
        //    cell = new PdfPCell(new Paragraph("UF", meuFont))
        //    {
        //        VerticalAlignment = Element.ALIGN_MIDDLE,
        //        HorizontalAlignment = Element.ALIGN_LEFT
        //    };
        //    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
        //    table.AddCell(cell);

        //    foreach (CLIENTE item in lista)
        //    {
        //        cell = new PdfPCell(new Paragraph(item.CATEGORIA_CLIENTE.CACL_NM_NOME, meuFont))
        //        {
        //            VerticalAlignment = Element.ALIGN_MIDDLE,
        //            HorizontalAlignment = Element.ALIGN_LEFT
        //        };
        //        table.AddCell(cell);
        //        cell = new PdfPCell(new Paragraph(item.CLIE_NM_NOME, meuFont))
        //        {
        //            VerticalAlignment = Element.ALIGN_MIDDLE,
        //            HorizontalAlignment = Element.ALIGN_LEFT
        //        };
        //        table.AddCell(cell);
        //        if (item.CLIE_NR_CPF != null)
        //        {
        //            cell = new PdfPCell(new Paragraph(item.CLIE_NR_CPF, meuFont))
        //            {
        //                VerticalAlignment = Element.ALIGN_MIDDLE,
        //                HorizontalAlignment = Element.ALIGN_LEFT
        //            };
        //            table.AddCell(cell);
        //        }
        //        else
        //        {
        //            cell = new PdfPCell(new Paragraph("-", meuFont))
        //            {
        //                VerticalAlignment = Element.ALIGN_MIDDLE,
        //                HorizontalAlignment = Element.ALIGN_LEFT
        //            };
        //            table.AddCell(cell);
        //        }
        //        if (item.CLIE_NR_CNPJ != null)
        //        {
        //            cell = new PdfPCell(new Paragraph(item.CLIE_NR_CNPJ, meuFont))
        //            {
        //                VerticalAlignment = Element.ALIGN_MIDDLE,
        //                HorizontalAlignment = Element.ALIGN_LEFT
        //            };
        //            table.AddCell(cell);
        //        }
        //        else
        //        {
        //            cell = new PdfPCell(new Paragraph("-", meuFont))
        //            {
        //                VerticalAlignment = Element.ALIGN_MIDDLE,
        //                HorizontalAlignment = Element.ALIGN_LEFT
        //            };
        //            table.AddCell(cell);
        //        }
        //        cell = new PdfPCell(new Paragraph(item.CLIE_NM_EMAIL, meuFont))
        //        {
        //            VerticalAlignment = Element.ALIGN_MIDDLE,
        //            HorizontalAlignment = Element.ALIGN_LEFT
        //        };
        //        table.AddCell(cell);
        //        if (item.CLIE_NR_TELEFONE != null)
        //        {
        //            cell = new PdfPCell(new Paragraph(item.CLIE_NR_TELEFONE, meuFont))
        //            {
        //                VerticalAlignment = Element.ALIGN_MIDDLE,
        //                HorizontalAlignment = Element.ALIGN_LEFT
        //            };
        //            table.AddCell(cell);
        //        }
        //        else
        //        {
        //            cell = new PdfPCell(new Paragraph("-", meuFont))
        //            {
        //                VerticalAlignment = Element.ALIGN_MIDDLE,
        //                HorizontalAlignment = Element.ALIGN_LEFT
        //            };
        //            table.AddCell(cell);
        //        }
        //        if (item.CLIE_NM_CIDADE != null)
        //        {
        //            cell = new PdfPCell(new Paragraph(item.CLIE_NM_CIDADE, meuFont))
        //            {
        //                VerticalAlignment = Element.ALIGN_MIDDLE,
        //                HorizontalAlignment = Element.ALIGN_LEFT
        //            };
        //            table.AddCell(cell);
        //        }
        //        else
        //        {
        //            cell = new PdfPCell(new Paragraph("-", meuFont))
        //            {
        //                VerticalAlignment = Element.ALIGN_MIDDLE,
        //                HorizontalAlignment = Element.ALIGN_LEFT
        //            };
        //            table.AddCell(cell);
        //        }
        //        if (item.UF != null)
        //        {
        //            cell = new PdfPCell(new Paragraph(item.UF.UF_SG_SIGLA, meuFont))
        //            {
        //                VerticalAlignment = Element.ALIGN_MIDDLE,
        //                HorizontalAlignment = Element.ALIGN_LEFT
        //            };
        //            table.AddCell(cell);
        //        }
        //        else
        //        {
        //            cell = new PdfPCell(new Paragraph("-", meuFont))
        //            {
        //                VerticalAlignment = Element.ALIGN_MIDDLE,
        //                HorizontalAlignment = Element.ALIGN_LEFT
        //            };
        //            table.AddCell(cell);
        //        }
        //    }
        //    pdfDoc.Add(table);

        //    // Linha Horizontal
        //    Paragraph line2 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLUE, Element.ALIGN_LEFT, 1)));
        //    pdfDoc.Add(line2);

        //    // Rodapé
        //    Chunk chunk1 = new Chunk("Parâmetros de filtro: ", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK));
        //    pdfDoc.Add(chunk1);

        //    String parametros = String.Empty;
        //    Int32 ja = 0;
        //    if (filtro != null)
        //    {
        //        if (filtro.CACL_CD_ID > 0)
        //        {
        //            parametros += "Categoria: " + filtro.CACL_CD_ID;
        //            ja = 1;
        //        }
        //        if (filtro.CLIE_CD_ID > 0)
        //        {
        //            CLIENTE cli = baseApp.GetItemById(filtro.CLIE_CD_ID);
        //            if (ja == 0)
        //            {
        //                parametros += "Nome: " + cli.CLIE_NM_NOME;
        //                ja = 1;
        //            }
        //            else
        //            {
        //                parametros += " e Nome: " + cli.CLIE_NM_NOME;
        //            }
        //        }
        //        if (filtro.CLIE_NR_CPF != null)
        //        {
        //            if (ja == 0)
        //            {
        //                parametros += "CPF: " + filtro.CLIE_NR_CPF;
        //                ja = 1;
        //            }
        //            else
        //            {
        //                parametros += " e CPF: " + filtro.CLIE_NR_CPF;
        //            }
        //        }
        //        if (filtro.CLIE_NR_CNPJ != null)
        //        {
        //            if (ja == 0)
        //            {
        //                parametros += "CNPJ: " + filtro.CLIE_NR_CNPJ;
        //                ja = 1;
        //            }
        //            else
        //            {
        //                parametros += " e CNPJ: " + filtro.CLIE_NR_CNPJ;
        //            }
        //        }
        //        if (filtro.CLIE_NM_EMAIL != null)
        //        {
        //            if (ja == 0)
        //            {
        //                parametros += "E-Mail: " + filtro.CLIE_NM_EMAIL;
        //                ja = 1;
        //            }
        //            else
        //            {
        //                parametros += " e E-Mail: " + filtro.CLIE_NM_EMAIL;
        //            }
        //        }
        //        if (filtro.CLIE_NM_CIDADE != null)
        //        {
        //            if (ja == 0)
        //            {
        //                parametros += "Cidade: " + filtro.CLIE_NM_CIDADE;
        //                ja = 1;
        //            }
        //            else
        //            {
        //                parametros += " e Cidade: " + filtro.CLIE_NM_CIDADE;
        //            }
        //        }
        //        if (filtro.UF != null)
        //        {
        //            if (ja == 0)
        //            {
        //                parametros += "UF: " + filtro.UF.UF_SG_SIGLA;
        //                ja = 1;
        //            }
        //            else
        //            {
        //                parametros += " e UF: " + filtro.UF.UF_SG_SIGLA;
        //            }
        //        }
        //        if (ja == 0)
        //        {
        //            parametros = "Nenhum filtro definido.";
        //        }
        //    }
        //    else
        //    {
        //        parametros = "Nenhum filtro definido.";
        //    }
        //    Chunk chunk = new Chunk(parametros, FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK));
        //    pdfDoc.Add(chunk);

        //    // Linha Horizontal
        //    Paragraph line3 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLUE, Element.ALIGN_LEFT, 1)));
        //    pdfDoc.Add(line3);

        //    // Finaliza
        //    pdfWriter.CloseStream = false;
        //    pdfDoc.Close();
        //    Response.Buffer = true;
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=" + nomeRel);
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Write(pdfDoc);
        //    Response.End();

        //    return RedirectToAction("MontarTelaCliente");
        //}



































    }
}