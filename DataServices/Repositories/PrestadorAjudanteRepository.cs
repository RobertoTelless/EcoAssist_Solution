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
    public class PrestadorAjudanteRepository : RepositoryBase<PRESTADOR_AJUDANTE>, IPrestadorAjudanteRepository
    {
        public List<PRESTADOR_AJUDANTE> GetAllItens()
        {
            return Db.PRESTADOR_AJUDANTE.ToList();
        }

        public PRESTADOR_AJUDANTE GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_AJUDANTE> query = Db.PRESTADOR_AJUDANTE.Where(p => p.PRAJ_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 