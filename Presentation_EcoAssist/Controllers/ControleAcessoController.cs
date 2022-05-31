using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationServices.Interfaces;
using EntitiesServices.Model;
using System.Globalization;
using SMS_Presentation.App_Start;
using EntitiesServices.Work_Classes;
using AutoMapper;
using ERP_CRM_Solution.ViewModels;
using System.IO;

namespace Presentation_EcoAssist.Controllers
{
    public class ControleAcessoController : Controller
    {
        private readonly IUsuarioAppService baseApp;
        private String msg;
        private Exception exception;
        USUARIO_SUGESTAO objeto = new USUARIO_SUGESTAO();
        USUARIO_SUGESTAO objetoAntes = new USUARIO_SUGESTAO();
        List<USUARIO_SUGESTAO> listaMaster = new List<USUARIO_SUGESTAO>();

        public ControleAcessoController(IUsuarioAppService baseApps)
        {
            baseApp = baseApps;
        }

        [HttpGet]
        public ActionResult Index()
        {
            USUARIO_SUGESTAO item = new USUARIO_SUGESTAO();
            UsuarioViewModel vm = Mapper.Map<USUARIO_SUGESTAO, UsuarioViewModel>(item);
            return View(vm);
        }

        [HttpGet]
        public ActionResult Login()
        {
            USUARIO_SUGESTAO item = new USUARIO_SUGESTAO();
            UsuarioLoginViewModel vm = Mapper.Map<USUARIO_SUGESTAO, UsuarioLoginViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioLoginViewModel vm)
        {
            try
            {
                // Checa credenciais e atualiza acessos
                USUARIO_SUGESTAO usuario;
                Session["UserCredentials"] = null;
                ViewBag.Usuario = null;
                USUARIO_SUGESTAO login = Mapper.Map<UsuarioLoginViewModel, USUARIO_SUGESTAO>(vm);
                Int32 volta = baseApp.ValidateLogin(login.USUA_NM_LOGIN, login.USUA_NM_SENHA, out usuario);
                if (volta == 1)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0001", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 2)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0002", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 3)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0003", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 5)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0005", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 4)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0004", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 6)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0006", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 7)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0007", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 9)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0073", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 10)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0109", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 11)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0012", CultureInfo.CurrentCulture));
                    return View(vm);
                }

                // Armazena credenciais para autorização
                Session["UserCredentials"] = usuario;
                Session["Usuario"] = usuario;
                Session["IdAssinante"] = 1;
                Session["Assinante"] = null;

                // Atualiza view
                String frase = String.Empty;
                String nome = usuario.USUA_NM_NOME;
                if (DateTime.Now.Hour <= 12)
                {
                    frase = "Bom dia, " + nome;
                }
                else if (DateTime.Now.Hour > 12 & DateTime.Now.Hour <= 18)
                {
                    frase = "Boa tarde, " + nome;
                }
                else
                {
                    frase = "Boa noite, " + nome;
                }

                ViewBag.Greeting = frase;
                ViewBag.Nome = usuario.USUA_NM_NOME;
                if (usuario.CARGO != null)
                {
                    ViewBag.Cargo = usuario.CARGO.CARG_NM_NOME;
                    Session["Cargo"] = usuario.CARGO.CARG_NM_NOME;
                }
                else
                {
                    ViewBag.Cargo = "-";
                    Session["Cargo"] = "-";
                }
                ViewBag.Foto = usuario.USUA_AQ_FOTO;

                Session["Greeting"] = frase;
                Session["Nome"] = usuario.USUA_NM_NOME;
                Session["Foto"] = usuario.USUA_AQ_FOTO;
                Session["Perfil"] = usuario.PERFIL;
                Session["PerfilSigla"] = usuario.PERFIL.PERF_SG_SIGLA;
                Session["PerfilSistema"] = usuario.USUA_IN_SISTEMA;
                Session["FlagInicial"] = 0;
                Session["FiltroData"] = 1;
                Session["FiltroStatus"] = 1;
                Session["Ativa"] = "1";
                Session["Login"] = 1;
                Session["IdAssinante"] = 1;
                Session["IdUsuario"] = usuario.USUA_CD_ID;
                Session["ListaMensagem"] = null;

                // Route
                if (usuario.USUA_IN_PROVISORIO == 1)
                {
                    return RedirectToAction("TrocarSenhaInicio", "ControleAcesso");
                }
                if ((USUARIO_SUGESTAO)Session["UserCredentials"] != null)
                {
                    return RedirectToAction("CarregarBase", "BaseAdmin");
                }
                return RedirectToAction("Login", "ControleAcesso");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(vm);
            }
        }

        public ActionResult Logout()
        {
            Session["UserCredentials"] = null;
            Session.Clear();
            return RedirectToAction("Login", "ControleAcesso");
        }

        public ActionResult Cancelar()
        {
            return RedirectToAction("Login", "ControleAcesso");
        }

        [HttpGet]
        public ActionResult TrocarSenha()
        {
            // Verifica se tem usuario logado
            USUARIO_SUGESTAO usuario = new USUARIO_SUGESTAO();
            if ((USUARIO_SUGESTAO)Session["UserCredentials"] != null)
            {
                usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Reseta senhas
            usuario.USUA_NM_NOVA_SENHA = null;
            usuario.USUA_NM_SENHA_CONFIRMA = null;
            UsuarioLoginViewModel vm = Mapper.Map<USUARIO_SUGESTAO, UsuarioLoginViewModel>(usuario);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrocarSenha(UsuarioLoginViewModel vm)
        {
            try
            {
                // Checa credenciais e atualiza acessos
                USUARIO_SUGESTAO usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];
                USUARIO_SUGESTAO item = Mapper.Map<UsuarioLoginViewModel, USUARIO_SUGESTAO>(vm);
                Int32 volta = baseApp.ValidateChangePassword(item);
                if (volta == 1)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0008", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 2)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0009", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 3)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0009", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 4)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0074", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                Session["UserCredentials"] = null;
                return RedirectToAction("Login", "ControleAcesso");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult TrocarSenhaInicio()
        {
            // Verifica se tem usuario logado
            USUARIO_SUGESTAO usuario = new USUARIO_SUGESTAO();
            if ((USUARIO_SUGESTAO)Session["UserCredentials"] != null)
            {
                usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Reseta senhas
            usuario.USUA_NM_NOVA_SENHA = null;
            usuario.USUA_NM_SENHA_CONFIRMA = null;
            UsuarioLoginViewModel vm = Mapper.Map<USUARIO_SUGESTAO, UsuarioLoginViewModel>(usuario);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrocarSenhaInicio(UsuarioLoginViewModel vm)
        {
            try
            {
                // Checa credenciais e atualiza acessos
                USUARIO_SUGESTAO usuario = (USUARIO_SUGESTAO)Session["UserCredentials"];
                USUARIO_SUGESTAO item = Mapper.Map<UsuarioLoginViewModel, USUARIO_SUGESTAO>(vm);
                Int32 volta = baseApp.ValidateChangePassword(item);
                if (volta == 1)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0008", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 2)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0009", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 3)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0009", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                if (volta == 4)
                {
                    ModelState.AddModelError("", PlatMensagens_Resources.ResourceManager.GetString("M0074", CultureInfo.CurrentCulture));
                    return View(vm);
                }
                ViewBag.Message = PlatMensagens_Resources.ResourceManager.GetString("M0075", CultureInfo.CurrentCulture);
                Session["UserCredentials"] = null;
                return RedirectToAction("Login", "ControleAcesso");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult GerarSenha()
        {
            USUARIO_SUGESTAO item = new USUARIO_SUGESTAO();
            UsuarioLoginViewModel vm = Mapper.Map<USUARIO_SUGESTAO, UsuarioLoginViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult GerarSenha(UsuarioLoginViewModel vm)
        {
            try
            {
                // Processa
                Session["UserCredentials"] = null;
                USUARIO_SUGESTAO item = Mapper.Map<UsuarioLoginViewModel, USUARIO_SUGESTAO>(vm);
                Int32 volta = baseApp.GenerateNewPassword(item.USUA_NM_EMAIL);
                if (volta == 1)
                {
                    return Json(PlatMensagens_Resources.ResourceManager.GetString("M0001", CultureInfo.CurrentCulture));
                }
                if (volta == 2)
                {
                    return Json(PlatMensagens_Resources.ResourceManager.GetString("M0096", CultureInfo.CurrentCulture));
                }
                if (volta == 3)
                {
                    return Json(PlatMensagens_Resources.ResourceManager.GetString("M0003", CultureInfo.CurrentCulture));
                }
                if (volta == 4)
                {
                    return Json(PlatMensagens_Resources.ResourceManager.GetString("M0004", CultureInfo.CurrentCulture));
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return Json(ex.Message);
            }
        }
    }
}