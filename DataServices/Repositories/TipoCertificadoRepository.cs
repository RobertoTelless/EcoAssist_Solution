using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;

namespace DataServices.Repositories
{
    public class TipoCertificadoRepository : RepositoryBase<TIPO_CERTIFICADO>, ITipoCertificadoRepository
    {
        public TIPO_CERTIFICADO GetItemById(Int32 id)
        {
            IQueryable<TIPO_CERTIFICADO> query = Db.TIPO_CERTIFICADO;
            query = query.Where(p => p.TICE_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<TIPO_CERTIFICADO> GetAllItens()
        {
            IQueryable<TIPO_CERTIFICADO> query = Db.TIPO_CERTIFICADO;
            return query.ToList();
        }
    }
}
 