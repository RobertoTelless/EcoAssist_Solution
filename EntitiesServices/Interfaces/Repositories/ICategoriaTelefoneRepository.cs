using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ICategoriaTelefoneRepository : IRepositoryBase<CATEGORIA_TELEFONE>
    {
        List<CATEGORIA_TELEFONE> GetAllItens();
        CATEGORIA_TELEFONE GetItemById(Int32 id);
        List<CATEGORIA_TELEFONE> GetAllItensAdm();
    }
}
