using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ITipoCertificadoRepository : IRepositoryBase<TIPO_CERTIFICADO>
    {
        List<TIPO_CERTIFICADO> GetAllItens();
        TIPO_CERTIFICADO GetItemById(Int32 id);
    }
}
