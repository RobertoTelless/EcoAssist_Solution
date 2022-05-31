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
    public class PrestadorBancoRepository : RepositoryBase<PRESTADOR_BANCO>, IPrestadorBancoRepository
    {
        public List<PRESTADOR_BANCO> GetAllItens()
        {
            return Db.PRESTADOR_BANCO.ToList();
        }

        public PRESTADOR_BANCO GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_BANCO> query = Db.PRESTADOR_BANCO.Where(p => p.PRBA_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 