using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IPrestadorCertificadoRepository : IRepositoryBase<PRESTADOR_CERTIFICADO>
    {
        List<PRESTADOR_CERTIFICADO> GetAllItens();
        PRESTADOR_CERTIFICADO GetItemById(Int32 id);
    }
}
