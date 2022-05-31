using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;

namespace DataServices.Repositories
{
    public class TipoVeiculoRepository : RepositoryBase<TIPO_VEICULO>, ITipoVeiculoRepository
    {
        public TIPO_VEICULO GetItemById(Int32 id)
        {
            IQueryable<TIPO_VEICULO> query = Db.TIPO_VEICULO;
            query = query.Where(p => p.TIVE_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<TIPO_VEICULO> GetAllItens()
        {
            IQueryable<TIPO_VEICULO> query = Db.TIPO_VEICULO;
            return query.ToList();
        }
    }
}
 