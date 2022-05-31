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
    public class PrestadorEnderecoRepository : RepositoryBase<PRESTADOR_ENDERECO>, IPrestadorEnderecoRepository
    {
        public List<PRESTADOR_ENDERECO> GetAllItens()
        {
            return Db.PRESTADOR_ENDERECO.ToList();
        }

        public PRESTADOR_ENDERECO GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_ENDERECO> query = Db.PRESTADOR_ENDERECO.Where(p => p.PREN_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 