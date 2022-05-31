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
    public class PrestadorMotoristaRepository : RepositoryBase<PRESTADOR_MOTORISTA>, IPrestadorMotoristaRepository
    {
        public List<PRESTADOR_MOTORISTA> GetAllItens()
        {
            return Db.PRESTADOR_MOTORISTA.ToList();
        }

        public PRESTADOR_MOTORISTA GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_MOTORISTA> query = Db.PRESTADOR_MOTORISTA.Where(p => p.PRMO_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 