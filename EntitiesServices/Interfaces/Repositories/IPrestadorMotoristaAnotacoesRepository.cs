using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IPrestadorMotoristaAnotacoesRepository : IRepositoryBase<PRESTADOR_MOTORISTA_ANOTACOES>
    {
        List<PRESTADOR_MOTORISTA_ANOTACOES> GetAllItens();
        PRESTADOR_MOTORISTA_ANOTACOES GetItemById(Int32 id);
    }
}
