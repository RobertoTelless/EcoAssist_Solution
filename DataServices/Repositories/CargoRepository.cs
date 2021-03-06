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
    public class CargoRepository : RepositoryBase<CARGO>, ICargoRepository
    {
        public CARGO CheckExist(CARGO conta)
        {
            IQueryable<CARGO> query = Db.CARGO;
            query = query.Where(p => p.CARG_NM_NOME == conta.CARG_NM_NOME);
            return query.FirstOrDefault();
        }

        public CARGO GetItemById(Int32 id)
        {
            IQueryable<CARGO> query = Db.CARGO;
            query = query.Where(p => p.CARG_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<CARGO> GetAllItensAdm()
        {
            IQueryable<CARGO> query = Db.CARGO;
            return query.ToList();
        }

        public List<CARGO> GetAllItens()
        {
            IQueryable<CARGO> query = Db.CARGO.Where(p => p.CARG_IN_ATIVO == 1);
            return query.ToList();
        }

    }
}
 