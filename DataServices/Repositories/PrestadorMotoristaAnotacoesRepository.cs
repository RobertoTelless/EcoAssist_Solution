using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class PrestadorMotoristaAnotacoesRepository : RepositoryBase<PRESTADOR_MOTORISTA_ANOTACOES>, IPrestadorMotoristaAnotacoesRepository
    {
        public List<PRESTADOR_MOTORISTA_ANOTACOES> GetAllItens()
        {
            return Db.PRESTADOR_MOTORISTA_ANOTACOES.ToList();
        }

        public PRESTADOR_MOTORISTA_ANOTACOES GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_MOTORISTA_ANOTACOES> query = Db.PRESTADOR_MOTORISTA_ANOTACOES.Where(p => p.PRMA_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 