using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class RegiaoCoberturaRepository : RepositoryBase<REGIAO_COBERTURA>, IRegiaoCoberturaRepository
    {
        public REGIAO_COBERTURA GetItemById(Int32 id)
        {
            IQueryable<REGIAO_COBERTURA> query = Db.REGIAO_COBERTURA;
            query = query.Where(p => p.RECO_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<REGIAO_COBERTURA> GetAllItens()
        {
            IQueryable<REGIAO_COBERTURA> query = Db.REGIAO_COBERTURA.Where(p => p.RECO_IN_ATIVO == 1);
            return query.ToList();
        }

        public List<REGIAO_COBERTURA> GetAllItensAdm()
        {
            IQueryable<REGIAO_COBERTURA> query = Db.REGIAO_COBERTURA;
            return query.ToList();
        }
    }
}
