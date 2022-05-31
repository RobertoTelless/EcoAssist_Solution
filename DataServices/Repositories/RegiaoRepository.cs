using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class RegiaoRepository : RepositoryBase<REGIAO>, IRegiaoRepository
    {
        public REGIAO GetItemById(Int32 id)
        {
            IQueryable<REGIAO> query = Db.REGIAO;
            query = query.Where(p => p.REGI_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<REGIAO> GetAllItens()
        {
            IQueryable<REGIAO> query = Db.REGIAO.Where(p => p.REGI_IN_ATIVO == 1);
            return query.ToList();
        }

        public List<REGIAO> GetAllItensAdm()
        {
            IQueryable<REGIAO> query = Db.REGIAO;
            return query.ToList();
        }
    }
}
