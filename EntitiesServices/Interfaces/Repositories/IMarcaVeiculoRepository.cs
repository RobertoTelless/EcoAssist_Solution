using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IMarcaVeiculoRepository : IRepositoryBase<MARCA_VEICULO>
    {
        List<MARCA_VEICULO> GetAllItens();
        MARCA_VEICULO GetItemById(Int32 id);
    }
}
