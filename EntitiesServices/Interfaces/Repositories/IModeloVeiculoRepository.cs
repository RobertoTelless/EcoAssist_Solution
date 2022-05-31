using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IModeloVeiculoRepository : IRepositoryBase<MODELO_VEICULO>
    {
        List<MODELO_VEICULO> GetAllItens();
        MODELO_VEICULO GetItemById(Int32 id);
    }
}
