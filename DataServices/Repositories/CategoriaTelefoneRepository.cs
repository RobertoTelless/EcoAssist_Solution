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
    public class CategoriaTelefoneRepository : RepositoryBase<CATEGORIA_TELEFONE>, ICategoriaTelefoneRepository
    {
        public CATEGORIA_TELEFONE GetItemById(Int32 id)
        {
            IQueryable<CATEGORIA_TELEFONE> query = Db.CATEGORIA_TELEFONE;
            query = query.Where(p => p.CATE_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<CATEGORIA_TELEFONE> GetAllItensAdm()
        {
            IQueryable<CATEGORIA_TELEFONE> query = Db.CATEGORIA_TELEFONE;
            return query.ToList();
        }

        public List<CATEGORIA_TELEFONE> GetAllItens()
        {
            IQueryable<CATEGORIA_TELEFONE> query = Db.CATEGORIA_TELEFONE.Where(p => p.CATE_IN_ATIVO == 1);
            return query.ToList();
        }

    }
}
 