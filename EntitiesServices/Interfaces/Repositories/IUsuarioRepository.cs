using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<USUARIO_SUGESTAO>
    {
        USUARIO_SUGESTAO GetByEmail(String email);
        USUARIO_SUGESTAO GetByLogin(String login);
        USUARIO_SUGESTAO GetItemById(Int32 id);
        List<USUARIO_SUGESTAO> GetAllUsuarios();
        List<USUARIO_SUGESTAO> GetAllItens();
        List<USUARIO_SUGESTAO> GetAllItensBloqueados();
        List<USUARIO_SUGESTAO> GetAllItensAcessoHoje();
        List<USUARIO_SUGESTAO> GetAllUsuariosAdm();
        List<USUARIO_SUGESTAO> ExecuteFilter(Int32? perfilId, Int32? cargoId, String nome, String login, String email);
        USUARIO_SUGESTAO GetAdministrador();
        USUARIO_SUGESTAO GetByEmailOnly(String email);
        List<USUARIO_SUGESTAO> GetAllSistema();
    }
}
