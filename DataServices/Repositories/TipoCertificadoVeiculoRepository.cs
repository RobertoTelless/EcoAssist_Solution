using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;

namespace DataServices.Repositories
{
    public class TipoCertificadoVeiculoRepository : RepositoryBase<TIPO_CERTIFICADO_VEICULO>, ITipoCertificadoVeiculoRepository
    {
        public TIPO_CERTIFICADO_VEICULO GetItemById(Int32 id)
        {
            IQueryable<TIPO_CERTIFICADO_VEICULO> query = Db.TIPO_CERTIFICADO_VEICULO;
            query = query.Where(p => p.TICV_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<TIPO_CERTIFICADO_VEICULO> GetAllItens()
        {
            IQueryable<TIPO_CERTIFICADO_VEICULO> query = Db.TIPO_CERTIFICADO_VEICULO;
            return query.ToList();
        }
    }
}
 