using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface INotificacaoAppService : IAppServiceBase<NOTIFICACAO>
    {
        Int32 ValidateCreate(NOTIFICACAO item, USUARIO_SUGESTAO usuario);
        Int32 ValidateEdit(NOTIFICACAO item, NOTIFICACAO itemAntes, USUARIO_SUGESTAO usuario);
        Int32 ValidateEdit(NOTIFICACAO item, NOTIFICACAO itemAntes);
        Int32 ValidateDelete(NOTIFICACAO item, USUARIO_SUGESTAO usuario);
        Int32 ValidateReativar(NOTIFICACAO item, USUARIO_SUGESTAO usuario);

        NOTIFICACAO GetItemById(Int32 id);
        List<NOTIFICACAO> GetAllItens();
        List<NOTIFICACAO> GetAllItensAdm();
        List<NOTIFICACAO> GetAllItensUser(Int32 id);
        List<NOTIFICACAO> GetNotificacaoNovas(Int32 id);
        Int32 ExecuteFilter(String titulo, DateTime? data, String texto, out List<NOTIFICACAO> objeto);
        
        NOTIFICACAO_ANEXO GetAnexoById(Int32 id);
        List<CATEGORIA_NOTIFICACAO> GetAllCategorias();
    }
}
