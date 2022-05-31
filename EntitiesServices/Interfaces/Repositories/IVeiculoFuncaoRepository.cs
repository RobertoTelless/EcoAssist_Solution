using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IVeiculoFuncaoRepository : IRepositoryBase<VEICULO_FUNCAO>
    {
        List<VEICULO_FUNCAO> GetAllItens();
        VEICULO_FUNCAO GetItemById(Int32 id);
    }
}
