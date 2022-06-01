using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IPrestadorCnpjRepository : IRepositoryBase<PRESTADOR_QUADRO_SOCIETARIO>
    {
        PRESTADOR_QUADRO_SOCIETARIO CheckExist(PRESTADOR_QUADRO_SOCIETARIO cqs);
        List<PRESTADOR_QUADRO_SOCIETARIO> GetAllItens();
        List<PRESTADOR_QUADRO_SOCIETARIO> GetByPrestador(PRESTADOR item);

    }
}