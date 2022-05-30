using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ITelefoneRepository : IRepositoryBase<TELEFONE>
    {
        TELEFONE CheckExist(TELEFONE item);
        TELEFONE GetItemById(Int32 id);
        List<TELEFONE> GetAllItens();
        List<TELEFONE> GetAllItensAdm();
        List<TELEFONE> ExecuteFilter(Int32? catId, String nome, String telefone, String cidade, Int32? uf, String celular, String email);
    }
}
