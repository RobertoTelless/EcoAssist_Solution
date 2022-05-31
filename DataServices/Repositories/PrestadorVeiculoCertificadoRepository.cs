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
    public class PrestadorVeiculoCertificadoRepository : RepositoryBase<PRESTADOR_VEICULO_CERTIFICADO>, IPrestadorVeiculoCertificadoRepository
    {
        public List<PRESTADOR_VEICULO_CERTIFICADO> GetAllItens()
        {
            return Db.PRESTADOR_VEICULO_CERTIFICADO.ToList();
        }

        public PRESTADOR_VEICULO_CERTIFICADO GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_VEICULO_CERTIFICADO> query = Db.PRESTADOR_VEICULO_CERTIFICADO.Where(p => p.PRVC_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 