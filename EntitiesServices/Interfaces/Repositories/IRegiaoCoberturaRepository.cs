using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IRegiaoCoberturaRepository : IRepositoryBase<REGIAO_COBERTURA>
    {
        List<REGIAO_COBERTURA> GetAllItens();
        REGIAO_COBERTURA GetItemById(Int32 id);
        List<REGIAO_COBERTURA> GetAllItensAdm();
    }
}
