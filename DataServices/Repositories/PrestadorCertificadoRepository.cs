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
    public class PrestadorCertificadoRepository : RepositoryBase<PRESTADOR_CERTIFICADO>, IPrestadorCertificadoRepository
    {
        public List<PRESTADOR_CERTIFICADO> GetAllItens()
        {
            return Db.PRESTADOR_CERTIFICADO.ToList();
        }

        public PRESTADOR_CERTIFICADO GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_CERTIFICADO> query = Db.PRESTADOR_CERTIFICADO.Where(p => p.PRCE_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 