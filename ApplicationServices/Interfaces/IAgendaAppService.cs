using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IAgendaAppService : IAppServiceBase<AGENDA>
    {
        Int32 ValidateCreate(AGENDA perfil, USUARIO_SUGESTAO usuario);
        Int32 ValidateEdit(AGENDA perfil, AGENDA perfilAntes, USUARIO_SUGESTAO usuario);
        Int32 ValidateDelete(AGENDA perfil, USUARIO_SUGESTAO usuario);
        Int32 ValidateReativar(AGENDA perfil, USUARIO_SUGESTAO usuario);

        List<CATEGORIA_AGENDA> GetAllTipos();
        AGENDA_ANEXO GetAnexoById(Int32 id);

        List<AGENDA> GetByDate(DateTime data);
        List<AGENDA> GetByUser(Int32 id);
        List<AGENDA> GetAllItens();
        List<AGENDA> GetAllItensAdm();
        AGENDA GetItemById(Int32 id);
        Int32 ExecuteFilter(DateTime? data, Int32? cat, String titulo, String descricao, Int32 idUser, Int32 corp, out List<AGENDA> objeto);
    }
}
