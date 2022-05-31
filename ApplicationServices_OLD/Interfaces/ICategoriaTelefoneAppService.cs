using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface ICategoriaTelefoneAppService : IAppServiceBase<CATEGORIA_TELEFONE>
    {
        Int32 ValidateCreate(CATEGORIA_TELEFONE item, USUARIO_SUGESTAO usuario);
        Int32 ValidateEdit(CATEGORIA_TELEFONE item, CATEGORIA_TELEFONE itemAntes, USUARIO_SUGESTAO usuario);
        Int32 ValidateDelete(CATEGORIA_TELEFONE item, USUARIO_SUGESTAO usuario);
        Int32 ValidateReativar(CATEGORIA_TELEFONE item, USUARIO_SUGESTAO usuario);

        List<CATEGORIA_TELEFONE> GetAllItens();
        CATEGORIA_TELEFONE GetItemById(Int32 id);
        List<CATEGORIA_TELEFONE> GetAllItensAdm();

    }
}
