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
    public class PrestadorContatoRepository : RepositoryBase<PRESTADOR_CONTATO>, IPrestadorContatoRepository
    {
        public List<PRESTADOR_CONTATO> GetAllItens()
        {
            return Db.PRESTADOR_CONTATO.ToList();
        }

        public PRESTADOR_CONTATO GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_CONTATO> query = Db.PRESTADOR_CONTATO.Where(p => p.PRCO_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 