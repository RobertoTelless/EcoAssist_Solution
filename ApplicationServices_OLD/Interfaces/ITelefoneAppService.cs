using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface ITelefoneAppService : IAppServiceBase<TELEFONE>
    {
        Int32 ValidateCreate(TELEFONE perfil, USUARIO_SUGESTAO usuario);
        Int32 ValidateEdit(TELEFONE perfil, TELEFONE perfilAntes, USUARIO_SUGESTAO usuario);
        Int32 ValidateEdit(TELEFONE item, TELEFONE itemAntes);
        Int32 ValidateDelete(TELEFONE perfil, USUARIO_SUGESTAO usuario);
        Int32 ValidateReativar(TELEFONE perfil, USUARIO_SUGESTAO usuario);

        List<TELEFONE> GetAllItens();
        List<TELEFONE> GetAllItensAdm();
        TELEFONE GetItemById(Int32 id);
        TELEFONE CheckExist(TELEFONE conta);

        List<CATEGORIA_TELEFONE> GetAllTipos();
        Int32 ExecuteFilter(Int32? catId, String nome, String telefone, String cidade, Int32? uf, String celular, String email, out List<TELEFONE> objeto);
        List<UF> GetAllUF();
        UF GetUFbySigla(String sigla);

    }
}
