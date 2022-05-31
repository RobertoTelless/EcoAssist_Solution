using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ICategoriaCNHRepository : IRepositoryBase<CNH_CATEGORIA>
    {
        List<CNH_CATEGORIA> GetAllItens();
        CNH_CATEGORIA GetItemById(Int32 id);
        List<CNH_CATEGORIA> GetAllItensAdm();
    }
}
