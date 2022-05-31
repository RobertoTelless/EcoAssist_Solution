using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ITipoCertificadoVeiculoRepository : IRepositoryBase<TIPO_CERTIFICADO_VEICULO>
    {
        List<TIPO_CERTIFICADO_VEICULO> GetAllItens();
        TIPO_CERTIFICADO_VEICULO GetItemById(Int32 id);
    }
}
