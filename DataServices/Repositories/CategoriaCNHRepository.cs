using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class CategoriaCNHRepository : RepositoryBase<CNH_CATEGORIA>, ICategoriaCNHRepository
    {
        public CNH_CATEGORIA GetItemById(Int32 id)
        {
            IQueryable<CNH_CATEGORIA> query = Db.CNH_CATEGORIA;
            query = query.Where(p => p.CNCA_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<CNH_CATEGORIA> GetAllItens()
        {
            IQueryable<CNH_CATEGORIA> query = Db.CNH_CATEGORIA.Where(p => p.CNCA_IN_ATIVO == 1);
            return query.ToList();
        }

        public List<CNH_CATEGORIA> GetAllItensAdm()
        {
            IQueryable<CNH_CATEGORIA> query = Db.CNH_CATEGORIA;
            return query.ToList();
        }
    }
}
