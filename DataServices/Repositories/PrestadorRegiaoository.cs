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
    public class PrestadorRegiaoRepository : RepositoryBase<PRESTADOR_REGIAO>, IPrestadorRegiaoRepository
    {
        public List<PRESTADOR_REGIAO> GetAllItens()
        {
            return Db.PRESTADOR_REGIAO.ToList();
        }

        public PRESTADOR_REGIAO GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_REGIAO> query = Db.PRESTADOR_REGIAO.Where(p => p.PRRE_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 