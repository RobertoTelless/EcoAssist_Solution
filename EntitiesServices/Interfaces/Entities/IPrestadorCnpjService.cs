using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IPrestadorCnpjService : IServiceBase<PRESTADOR_QUADRO_SOCIETARIO>
    {
        PRESTADOR_QUADRO_SOCIETARIO CheckExist(PRESTADOR_QUADRO_SOCIETARIO cqs);
        List<PRESTADOR_QUADRO_SOCIETARIO> GetAllItens();
        List<PRESTADOR_QUADRO_SOCIETARIO> GetByPrestador(PRESTADOR item);

        Int32 Create(PRESTADOR_QUADRO_SOCIETARIO cqs, LOG log);
        Int32 Create(PRESTADOR_QUADRO_SOCIETARIO cqs);
    }
}