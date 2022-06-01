using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IPrestadorCnpjAppService : IAppServiceBase<PRESTADOR_QUADRO_SOCIETARIO>
    {
        List<PRESTADOR_QUADRO_SOCIETARIO> GetAllItens();
        List<PRESTADOR_QUADRO_SOCIETARIO> GetByPrestador(PRESTADOR item);
        Int32 ValidateCreate(PRESTADOR_QUADRO_SOCIETARIO item, USUARIO_SUGESTAO usuario);

    }
}