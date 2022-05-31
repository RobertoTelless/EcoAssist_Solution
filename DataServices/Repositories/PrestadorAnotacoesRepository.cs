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
    public class PrestadorAnotacoesRepository : RepositoryBase<PRESTADOR_ANOTACOES>, IPrestadorAnotacoesRepository
    {
        public List<PRESTADOR_ANOTACOES> GetAllItens()
        {
            return Db.PRESTADOR_ANOTACOES.ToList();
        }

        public PRESTADOR_ANOTACOES GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_ANOTACOES> query = Db.PRESTADOR_ANOTACOES.Where(p => p.PRAN_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 