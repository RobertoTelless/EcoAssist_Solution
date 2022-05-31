using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface ITelefoneService : IServiceBase<TELEFONE>
    {
        Int32 Create(TELEFONE perfil, LOG log);
        Int32 Create(TELEFONE perfil);
        Int32 Edit(TELEFONE perfil, LOG log);
        Int32 Edit(TELEFONE perfil);
        Int32 Delete(TELEFONE perfil, LOG log);

        TELEFONE CheckExist(TELEFONE conta);
        TELEFONE GetItemById(Int32 id);
        List<TELEFONE> GetAllItens();
        List<TELEFONE> GetAllItensAdm();

        List<CATEGORIA_TELEFONE> GetAllTipos();
        List<TELEFONE> ExecuteFilter(Int32? catId, String nome, String telefone, String cidade, Int32? uf, String celular, String email);
        List<UF> GetAllUF();
        UF GetUFbySigla(String sigla);
    }
}
