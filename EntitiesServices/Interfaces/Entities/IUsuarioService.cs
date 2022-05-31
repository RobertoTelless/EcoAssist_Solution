using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IUsuarioService : IServiceBase<USUARIO_SUGESTAO>
    {
        Boolean VerificarCredenciais(String senha, USUARIO_SUGESTAO usuario);
        USUARIO_SUGESTAO GetByEmail(String email);
        USUARIO_SUGESTAO GetByLogin(String login);
        USUARIO_SUGESTAO RetriveUserByEmail(String email);
        Int32 VerifyUserSubscription(USUARIO_SUGESTAO usuario);

        Int32 CreateUser(USUARIO_SUGESTAO usuario, LOG log);
        Int32 CreateUser(USUARIO_SUGESTAO usuario);
        Int32 EditUser(USUARIO_SUGESTAO usuario, LOG log);
        Int32 EditUser(USUARIO_SUGESTAO usuario);

        Endereco GetAdressCEP(string CEP);
        CONFIGURACAO CarregaConfiguracao(Int32 id);
        List<USUARIO_SUGESTAO> GetAllUsuariosAdm();
        USUARIO_SUGESTAO GetItemById(Int32 id);
        USUARIO_SUGESTAO GetAdministrador();
        List<USUARIO_SUGESTAO> GetAllUsuarios();
        List<PERFIL> GetAllPerfis();
        List<USUARIO_SUGESTAO> GetAllItens();
        List<USUARIO_SUGESTAO> GetAllItensBloqueados();
        List<USUARIO_SUGESTAO> GetAllItensAcessoHoje();
        List<USUARIO_SUGESTAO> ExecuteFilter(Int32? perfilId, Int32? cargoId, String nome, String login, String email);
        List<USUARIO_SUGESTAO> GetAllSistema();

        TEMPLATE GetTemplateByCode(String codigo);
        USUARIO_ANEXO GetAnexoById(Int32 id);
        List<NOTIFICACAO> GetAllItensUser(Int32 id);
        List<NOTIFICACAO> GetNotificacaoNovas(Int32 id);
        TEMPLATE GetTemplate(String code);
        List<CARGO> GetAllCargos();
    }
}
