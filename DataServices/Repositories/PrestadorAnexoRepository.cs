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
    public class PrestadorAnexoRepository : RepositoryBase<PRESTADOR_ANEXO>, IPrestadorAnexoRepository
    {
        public List<PRESTADOR_ANEXO> GetAllItens()
        {
            return Db.PRESTADOR_ANEXO.ToList();
        }

        public PRESTADOR_ANEXO GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_ANEXO> query = Db.PRESTADOR_ANEXO.Where(p => p.PRAN_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 