using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface INoticiaAppService : IAppServiceBase<NOTICIA>
    {
        Int32 ValidateCreate(NOTICIA item, USUARIO_SUGESTAO usuario);
        Int32 ValidateEdit(NOTICIA item, NOTICIA itemAntes, USUARIO_SUGESTAO usuario);
        Int32 ValidateEdit(NOTICIA item, NOTICIA itemAntes);
        Int32 ValidateDelete(NOTICIA item, USUARIO_SUGESTAO usuario);
        Int32 ValidateReativar(NOTICIA item, USUARIO_SUGESTAO usuario);

        NOTICIA GetItemById(Int32 id);
        List<NOTICIA> GetAllItens();
        List<NOTICIA> GetAllItensAdm();
        Int32 ExecuteFilter(String titulo, String autor, DateTime? data, String texto, String link, out List<NOTICIA> objeto);
        List<NOTICIA> GetAllItensValidos();
        NOTICIA_COMENTARIO GetComentarioById(Int32 id);

    }
}
