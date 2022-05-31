using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IRegiaoRepository : IRepositoryBase<REGIAO>
    {
        List<REGIAO> GetAllItens();
        REGIAO GetItemById(Int32 id);
        List<REGIAO> GetAllItensAdm();
    }
}
