using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IUsuarioAppService : IAppServiceBase<USUARIO_SUGESTAO>
    {
        USUARIO_SUGESTAO GetByEmail(String email);
        USUARIO_SUGESTAO GetByLogin(String login);
        List<USUARIO_SUGESTAO> GetAllUsuariosAdm();
        USUARIO_SUGESTAO GetItemById(Int32 id);
        List<USUARIO_SUGESTAO> GetAllUsuarios();
        List<USUARIO_SUGESTAO> GetAllItens();
        List<USUARIO_SUGESTAO> GetAllItensBloqueados();
        List<USUARIO_SUGESTAO> GetAllItensAcessoHoje();
        USUARIO_ANEXO GetAnexoById(Int32 id);
        List<NOTIFICACAO> GetAllItensUser(Int32 id);
        List<NOTIFICACAO> GetNotificacaoNovas(Int32 id);

        Int32 ValidateCreate(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado);
        Int32 ValidateCreateAssinante(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado);
        Int32 ValidateEdit(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioAntes, USUARIO_SUGESTAO usuarioLogado);
        Int32 ValidateEdit(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado);
        Int32 ValidateLogin(String email, String senha, out USUARIO_SUGESTAO usuario);
        Int32 ValidateDelete(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado);
        Int32 ValidateBloqueio(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado);
        Int32 ValidateDesbloqueio(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado);
        Int32 ValidateChangePassword(USUARIO_SUGESTAO usuario);
        Int32 ValidateReativar(USUARIO_SUGESTAO usuario, USUARIO_SUGESTAO usuarioLogado);

        Int32 GenerateNewPassword(String email);
        List<PERFIL> GetAllPerfis();
        Int32 ExecuteFilter(Int32? perfilId, Int32? cargoId, String nome, String login, String email, out List<USUARIO_SUGESTAO> objeto);
        USUARIO_SUGESTAO GetAdministrador();
        List<CARGO> GetAllCargos();
        List<USUARIO_SUGESTAO> GetAllSistema();

    }
}
