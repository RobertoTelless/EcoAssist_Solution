using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IPrestadorVeiculoRepository : IRepositoryBase<PRESTADOR_VEICULO>
    {
        List<PRESTADOR_VEICULO> GetAllItens();
        PRESTADOR_VEICULO GetItemById(Int32 id);
    }
}
