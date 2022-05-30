using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ApplicationServices.Interfaces;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Text.RegularExpressions;
//using SendGrid;
//using SendGrid.Helpers.Mail;
using System.Web;

namespace ApplicationServices.Services
{
    public class UsuarioAppService : AppServiceBase<USUARIO_SUGESTAO>, IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly INotificacaoService _notiService;

        public UsuarioAppService(IUsuarioService usuarioService, INotificacaoService notiService): base(usuarioService)
        {
            _usuarioService = usuarioService;
            _notiService = notiService;
        }

        public USUARIO_SUGESTAO GetByEmail(String email)
        {
            return _usuarioService.GetByEmail(email);
        }

        public USUARIO_SUGESTAO GetByLogin(String login)
        {
            return _usuarioService.GetByLogin(login);
        }

        public List<USUARIO_SUGESTAO> GetAllUsuariosAdm()
        {
            return _usuarioService.GetAllUsuariosAdm();
        }

        public USUARIO_SUGESTAO GetItemById(Int32 id)
        {
            return _usuarioService.GetItemById(id);
        }

        public List<USUARIO_SUGESTAO> GetAllUsuarios()
        {
            return _usuarioService.GetAllUsuarios();
        }

        public List<USUARIO_SUGESTAO> GetAllSistema()
        {
            return _usuarioService.GetAllSistema();
        }

        public List<USUARIO_SUGESTAO> GetAllItens()
        {
            return _usuarioService.GetAllItens();
        }

        public List<USUARIO_SUGESTAO> GetAllTecnicos()
        {
            return _usuarioService.GetAllTecnicos();
        }

        public List<CARGO> GetAllCargos()
        {
            return _usuarioService.GetAllCargos();
        }

        public USUARIO_SUGESTAO GetAdministrador()
        {
            return _usuarioService.GetAdministrador();
        }

        public USUARIO_SUGESTAO GetComprador()
        {
            return _usuarioService.GetComprador();
        }

        public USUARIO_SUGESTAO GetTecnico()
        {
            return _usuarioService.GetTecnico();
        }

        public USUARIO_SUGESTAO GetAprovador()
        {
            return _usuarioService.GetAprovador();
        }

        public List<NOTIFICACAO> GetAllItensUser(Int32 id)
        {
            return _usuarioService.GetAllItensUser(id);
        }

        public List<NOTIFICACAO> GetNotificacaoNovas(Int32 id)
        {
            return _usuarioService.GetNotificacaoNovas(id);
        }

        public List<USUARIO_SUGESTAO> GetAllItensBloqueados()
        {
            return _usuarioService.GetAllItensBloqueados();
        }

        public List<USUARIO_SUGESTAO> GetAllItensAcessoHoje()
        {
            return _usuarioService.GetAllItensAcessoHoje();
        }

        public USUARIO_ANEXO GetAnexoById(Int32 id)
        {
            return _usuarioService.GetAnexoById(id);
        }

        public Int32 ValidateCreate(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado)
        {
            try
            {
                // Verifica senhas
                if (usuario.USUA_NM_SENHA != usuario.USUA_NM_SENHA_CONFIRMA)
                {
                    return 1;
                }

                // Verifica Email
                if (!ValidarItensDiversos.IsValidEmail(usuario.USUA_NM_EMAIL))
                {
                    return 2;
                }

                // Verifica existencia prévia
                if (_usuarioService.GetByEmail(usuario.USUA_NM_EMAIL) != null)
                {
                    return 3;
                }
                if (_usuarioService.GetByLogin(usuario.USUA_NM_LOGIN) != null)
                {
                    return 4;
                }

                //Completa campos de usuários
                //usuario.USUA_NM_SENHA = Cryptography.Encode(usuario.USUA_NM_SENHA);
                usuario.USUA_IN_BLOQUEADO = 0;
                usuario.USUA_IN_PROVISORIO = 0;
                usuario.USUA_IN_LOGIN_PROVISORIO = 0;
                usuario.USUA_NR_ACESSOS = 0;
                usuario.USUA_NR_FALHAS = 0;
                usuario.USUA_DT_ALTERACAO = null;
                usuario.USUA_DT_BLOQUEADO = null;
                usuario.USUA_DT_TROCA_SENHA = null;
                usuario.USUA_DT_ACESSO = DateTime.Now;
                usuario.USUA_DT_CADASTRO = DateTime.Today.Date;
                usuario.USUA_IN_ATIVO = 1;
                usuario.USUA_DT_ULTIMA_FALHA = DateTime.Now;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                    LOG_NM_OPERACAO = "AddUSUA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO_SUGESTAO>(usuario),
                    LOG_IN_ATIVO = 1
                };


                // Persiste
                Int32 volta = _usuarioService.CreateUser(usuario, log);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateAssinante(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado)
        {
            try
            {
                // Verifica senhas
                if (usuario.USUA_NM_SENHA != usuario.USUA_NM_SENHA_CONFIRMA)
                {
                    return 1;
                }

                // Verifica Email
                if (!ValidarItensDiversos.IsValidEmail(usuario.USUA_NM_EMAIL))
                {
                    return 2;
                }

                // Verifica existencia prévia
                if (_usuarioService.GetByEmail(usuario.USUA_NM_EMAIL) != null)
                {
                    return 3;
                }
                if (_usuarioService.GetByLogin(usuario.USUA_NM_LOGIN) != null)
                {
                    return 4;
                }

                //Completa campos de usuários
                //usuario.USUA_NM_SENHA = Cryptography.Encode(usuario.USUA_NM_SENHA);
                usuario.USUA_IN_BLOQUEADO = 0;
                usuario.USUA_IN_PROVISORIO = 0;
                usuario.USUA_IN_LOGIN_PROVISORIO = 0;
                usuario.USUA_NR_ACESSOS = 0;
                usuario.USUA_NR_FALHAS = 0;
                usuario.USUA_DT_ALTERACAO = null;
                usuario.USUA_DT_BLOQUEADO = null;
                usuario.USUA_DT_TROCA_SENHA = null;
                usuario.USUA_DT_ACESSO = DateTime.Now;
                usuario.USUA_DT_CADASTRO = DateTime.Today.Date;
                usuario.USUA_IN_ATIVO = 1;
                usuario.USUA_DT_ULTIMA_FALHA = DateTime.Now;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                    LOG_NM_OPERACAO = "AddUSUA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO_SUGESTAO>(usuario),
                    LOG_IN_ATIVO = 1
                };


                // Persiste
                Int32 volta = _usuarioService.CreateUser(usuario, log);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioAntes, USUARIO_SUGESTAO usuarioLogado)
        {
            try
            {
                // Verifica Email
                if (!ValidarItensDiversos.IsValidEmail(usuario.USUA_NM_EMAIL))
                {
                    return 1;
                }

                // Verifica existencia prévia
                USUARIO_SUGESTAO usu = _usuarioService.GetByEmail(usuario.USUA_NM_EMAIL);
                if (usu != null)
                {
                    if (usu.USUA_CD_ID != usuario.USUA_CD_ID)
                    {
                        return 2;
                    }
                }
                usu = _usuarioService.GetByLogin(usuario.USUA_NM_LOGIN);
                if (usu != null)
                {
                    if (usu.USUA_CD_ID != usuario.USUA_CD_ID)
                    {
                        return 3;
                    }
                }

                //Acerta campos de usuários
                usuario.USUA_DT_ALTERACAO = DateTime.Now;
                usuario.USUA_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                    LOG_NM_OPERACAO = "EditUSUA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO_SUGESTAO>(usuario),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<USUARIO_SUGESTAO>(usuarioAntes),
                    LOG_IN_ATIVO = 1
                };


                // Persiste
                Int32 volta = _usuarioService.EditUser(usuario);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado)
        {
            try
            {
                // Verifica Email
                if (!ValidarItensDiversos.IsValidEmail(usuario.USUA_NM_EMAIL))
                {
                    return 1;
                }

                // Verifica existencia prévia
                USUARIO_SUGESTAO usu = _usuarioService.GetByEmail(usuario.USUA_NM_EMAIL);
                if (usu != null)
                {
                    if (usu.USUA_CD_ID != usuario.USUA_CD_ID)
                    {
                        return 2;
                    }
                }

                //Acerta campos de usuários
                usuario.USUA_DT_ALTERACAO = DateTime.Now;
                usuario.USUA_IN_ATIVO = 1;

                // Persiste
                Int32 volta = _usuarioService.EditUser(usuario);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Int32 ValidateDelete(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado)
        {
            try
            {
                // Verifica integridade

                // Acerta campos de usuários
                usuario.USUA_DT_ALTERACAO = DateTime.Now;
                usuario.USUA_IN_ATIVO = 0;

                // Monta Log
                //LOG log = new LOG
                //{
                //    LOG_DT_DATA = DateTime.Now,
                //    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                //    ASSI_CD_ID = SessionMocks.IdAssinante,
                //    LOG_NM_OPERACAO = "DelUSUA",
                //    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO>(usuario),
                //    LOG_IN_ATIVO = 1
                //};

                // Persiste
                return _usuarioService.EditUser(usuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado)
        {
            try
            {
                // Verifica integridade

                // Acerta campos de usuários
                usuario.USUA_DT_ALTERACAO = DateTime.Now;
                usuario.USUA_IN_ATIVO = 1;

                // Monta Log
                //LOG log = new LOG
                //{
                //    LOG_DT_DATA = DateTime.Now,
                //    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                //    ASSI_CD_ID = SessionMocks.IdAssinante,
                //    LOG_NM_OPERACAO = "ReatUSUA",
                //    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO>(usuario),
                //    LOG_IN_ATIVO = 1
                //};

                // Persiste
                return _usuarioService.EditUser(usuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateBloqueio(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado)
        {
            try
            {
                //Acerta campos de usuários
                usuario.USUA_DT_BLOQUEADO = DateTime.Today;
                usuario.USUA_IN_BLOQUEADO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                    LOG_NM_OPERACAO = "BlqUSUA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO_SUGESTAO>(usuario),
                    LOG_IN_ATIVO = 1
                };

                // Persiste
                return _usuarioService.EditUser(usuario, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDesbloqueio(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado)
        {
            try
            {
                //Acerta campos de usuários
                usuario.USUA_DT_BLOQUEADO = DateTime.Now;
                usuario.USUA_IN_BLOQUEADO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                    LOG_NM_OPERACAO = "DbqUSUA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO_SUGESTAO>(usuario),
                    LOG_IN_ATIVO = 1
                };

                // Persiste
                return _usuarioService.EditUser(usuario, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateLogin(String login, String senha, out USUARIO_SUGESTAO usuario)
        {
            try
            {
                usuario = new USUARIO_SUGESTAO();
                // Checa preenchimemto
                if (String.IsNullOrEmpty(login))
                {
                    return 10;
                }
                if (String.IsNullOrEmpty(senha))
                {
                    return 9;
                }

                // Checa existencia
                usuario = _usuarioService.GetByLogin(login);
                if (usuario == null)
                {
                    usuario = new USUARIO_SUGESTAO();
                    return 2;
                }

                // Verifica se está ativo
                if (usuario.USUA_IN_ATIVO != 1)
                {
                    return 3;
                }

                // Verifica se acessa sistema
                if (usuario.USUA_IN_SISTEMA == 0)
                {
                    return 11;
                }


                // Verifica se está bloqueado
                if (usuario.USUA_IN_BLOQUEADO == 1)
                {
                    return 4;
                }

                // verifica senha proviória
                if (usuario.USUA_IN_PROVISORIO == 1)
                {
                    if (usuario.USUA_IN_LOGIN_PROVISORIO == 0)
                    {
                        usuario.USUA_IN_LOGIN_PROVISORIO = 1;
                    }
                    else
                    {
                        return 5;
                    }
                }

                // Verifica credenciais
                Boolean retorno = _usuarioService.VerificarCredenciais(senha, usuario);
                if (!retorno)
                {
                    if (usuario.USUA_NR_FALHAS <= _usuarioService.CarregaConfiguracao(1).CONF_NR_FALHAS_DIA)
                    {
                        if (usuario.USUA_DT_ULTIMA_FALHA != null)
                        {
                            if (usuario.USUA_DT_ULTIMA_FALHA.Value.Date != DateTime.Now.Date)
                            {
                                usuario.USUA_DT_ULTIMA_FALHA = DateTime.Now.Date;
                                usuario.USUA_NR_FALHAS = 1;
                            }
                            else
                            {
                                usuario.USUA_NR_FALHAS++;
                            }
                        }
                        else
                        {
                            usuario.USUA_DT_ULTIMA_FALHA = DateTime.Today.Date;
                            usuario.USUA_NR_FALHAS = 1;
                        }

                    }
                    else if (usuario.USUA_NR_FALHAS > _usuarioService.CarregaConfiguracao(1).CONF_NR_FALHAS_DIA)
                    {
                        usuario.USUA_DT_BLOQUEADO = DateTime.Today.Date;
                        usuario.USUA_IN_BLOQUEADO = 1;
                        usuario.USUA_NR_FALHAS = 0;
                        usuario.USUA_DT_ULTIMA_FALHA = DateTime.Today.Date;
                        Int32 voltaBloqueio = _usuarioService.EditUser(usuario);
                        return 6;
                    }
                    Int32 volta = _usuarioService.EditUser(usuario);
                    return 7;
                }

                // Atualiza acessos e data do acesso
                usuario.USUA_NR_ACESSOS = ++usuario.USUA_NR_ACESSOS;
                usuario.USUA_DT_ACESSO = DateTime.Now.Date;
                Int32 voltaAcesso = _usuarioService.EditUser(usuario);
                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateChangePassword(USUARIO_SUGESTAO usuario)
        {
            try
            {
                // Checa preenchimento
                if (String.IsNullOrEmpty(usuario.USUA_NM_NOVA_SENHA))
                {
                    return 3;
                }
                if (String.IsNullOrEmpty(usuario.USUA_NM_SENHA_CONFIRMA))
                {
                    return 4;
                }

                // Verifica se senha igual a anterior
                if (usuario.USUA_NM_SENHA == usuario.USUA_NM_NOVA_SENHA)
                {
                    return 1;
                }

                // Verifica se senha foi confirmada
                if (usuario.USUA_NM_NOVA_SENHA != usuario.USUA_NM_SENHA_CONFIRMA)
                {
                    return 2;
                }

                //Completa e acerta campos 
                //usuario.USUA_NM_SENHA = Cryptography.Encode(usuario.USUA_NM_NOVA_SENHA);
                usuario.USUA_NM_SENHA = usuario.USUA_NM_NOVA_SENHA;
                usuario.USUA_DT_TROCA_SENHA = DateTime.Now.Date;
                usuario.USUA_IN_BLOQUEADO = 0;
                usuario.USUA_IN_PROVISORIO = 0;
                usuario.USUA_IN_LOGIN_PROVISORIO = 0;
                usuario.USUA_NR_ACESSOS = 0;
                usuario.USUA_NR_FALHAS = 0;
                usuario.USUA_DT_ALTERACAO = null;
                usuario.USUA_DT_BLOQUEADO = null;
                usuario.USUA_DT_TROCA_SENHA = null;
                usuario.USUA_DT_ACESSO = DateTime.Now;
                usuario.USUA_DT_CADASTRO = DateTime.Now;
                usuario.USUA_IN_ATIVO = 1;
                usuario.USUA_DT_ULTIMA_FALHA = null;

                // Gera Notificação
                NOTIFICACAO noti = new NOTIFICACAO();
                noti.CANO_CD_ID = 1;
                noti.NOTI_DT_EMISSAO = DateTime.Today;
                noti.NOTI_DT_VALIDADE = DateTime.Today.Date.AddDays(30);
                noti.NOTI_IN_VISTA = 0;
                noti.NOTI_NM_TITULO = "Alteração de Senha";
                noti.NOTI_IN_ATIVO = 1;
                noti.NOTI_TX_TEXTO = "ATENÇÃO: A sua senha foi alterada em " + DateTime.Today.Date.ToLongDateString() + ".";
                noti.USUA_CD_ID = usuario.USUA_CD_ID;
                noti.NOTI_IN_STATUS = 1;
                noti.NOTI_IN_NIVEL = 1;
                Int32 volta1 = _notiService.Create(noti);


                //Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    LOG_NM_OPERACAO = "ChangePWD",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO_SUGESTAO>(usuario),
                };

                // Persiste
                return _usuarioService.EditUser(usuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 GenerateNewPassword(String email)
        {
            // Checa email
            if (!ValidarItensDiversos.IsValidEmail(email))
            {
                return 1;
            }
            USUARIO_SUGESTAO usuario = _usuarioService.RetriveUserByEmail(email);
            if (usuario == null)
            {
                return 2;
            }

            // Verifica se usuário está ativo
            if (usuario.USUA_IN_ATIVO == 0)
            {
                return 3;
            }

            // Verifica se usuário não está bloqueado
            if (usuario.USUA_IN_BLOQUEADO == 1)
            {
                return 4;
            }

            // Gera nova senha
            String senha = Cryptography.GenerateRandomPassword(6);

            // Atauliza objeto
            //usuario.USUA_NM_SENHA = Cryptography.Encode(senha);
            usuario.USUA_NM_SENHA = senha;
            usuario.USUA_IN_PROVISORIO = 1;
            usuario.USUA_DT_ALTERACAO = DateTime.Now;
            usuario.USUA_DT_TROCA_SENHA = DateTime.Now;
            usuario.USUA_IN_LOGIN_PROVISORIO = 0;

            // Monta log
            LOG log = new LOG();
            log.LOG_DT_DATA = DateTime.Now;
            log.LOG_NM_OPERACAO = "NewPWD";
            log.LOG_TX_REGISTRO = senha;
            log.LOG_IN_ATIVO = 1;

            // Gera Notificação
            NOTIFICACAO noti = new NOTIFICACAO();
            noti.CANO_CD_ID = 1;
            noti.NOTI_DT_EMISSAO = DateTime.Today;
            noti.NOTI_DT_VALIDADE = DateTime.Today.Date.AddDays(30);
            noti.NOTI_IN_VISTA = 0;
            noti.NOTI_NM_TITULO = "Geração de Nova Senha";
            noti.NOTI_IN_ATIVO = 1;
            noti.NOTI_TX_TEXTO = "ATENÇÃO: Sua solicitação de nova senha foi atendida em " + DateTime.Today.Date.ToLongDateString() + ". Verifique no seu e-mail cadastrado no sistema.";
            noti.USUA_CD_ID = usuario.USUA_CD_ID;
            noti.NOTI_IN_STATUS = 1;
            noti.NOTI_IN_NIVEL = 1;
            Int32 volta1 = _notiService.Create(noti);

            // Recupera template e-mail
            String header = _usuarioService.GetTemplate("NEWPWD").TEMP_TX_CABECALHO;
            String body = _usuarioService.GetTemplate("NEWPWD").TEMP_TX_CORPO;
            String data = _usuarioService.GetTemplate("NEWPWD").TEMP_TX_DADOS;

            // Prepara dados do e-mail  
            header = header.Replace("{Nome}", usuario.USUA_NM_NOME);
            data = data.Replace("{Data}", usuario.USUA_DT_TROCA_SENHA.Value.ToLongDateString());
            data = data.Replace("{Senha}", usuario.USUA_NM_SENHA);

            // Concatena
            String emailBody = header + body + data;

            // Prepara e-mail e enviar
            CONFIGURACAO conf = _usuarioService.CarregaConfiguracao(1);
            Email mensagem = new Email();
            mensagem.ASSUNTO = "Geração de Nova Senha";
            mensagem.CORPO = emailBody;
            mensagem.DEFAULT_CREDENTIALS = false;
            mensagem.EMAIL_DESTINO = usuario.USUA_NM_EMAIL;
            mensagem.EMAIL_EMISSOR = conf.CONF_NM_EMAIL_EMISSOO;
            mensagem.ENABLE_SSL = true;
            mensagem.NOME_EMISSOR = "Sistema";
            mensagem.PORTA = conf.CONF_NM_PORTA_SMTP;
            mensagem.PRIORIDADE = System.Net.Mail.MailPriority.High;
            mensagem.SENHA_EMISSOR = conf.CONF_NM_SENHA_EMISSOR;
            mensagem.SMTP = conf.CONF_NM_HOST_SMTP;

            // Envia e-mail
            Int32 voltaMail = CommunicationPackage.SendEmail(mensagem);
            
            // Atualiza usuario
            Int32 volta = _usuarioService.EditUser(usuario);

            // Retorna sucesso
            return 0;
        }

        public Int32 ExecuteFilter(Int32? perfilId, Int32? cargoId, String nome, String login, String email, out List<USUARIO_SUGESTAO> objeto)
        {
            try
            {
                objeto = new List<USUARIO_SUGESTAO>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _usuarioService.ExecuteFilter(perfilId, cargoId, nome, login, email);
                if (objeto.Count == 0)
                {
                    volta = 1;
                }
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PERFIL> GetAllPerfis()
        {
            List<PERFIL> lista = _usuarioService.GetAllPerfis();
            return lista;
        }
    }
}
