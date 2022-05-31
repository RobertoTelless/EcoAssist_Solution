using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IPrestadorAnotacoesRepository : IRepositoryBase<PRESTADOR_ANOTACOES>
    {
        List<PRESTADOR_ANOTACOES> GetAllItens();
        PRESTADOR_ANOTACOES GetItemById(Int32 id);
    }
}
