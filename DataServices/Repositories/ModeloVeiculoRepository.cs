using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;

namespace DataServices.Repositories
{
    public class ModeloVeiculoRepository : RepositoryBase<MODELO_VEICULO>, IModeloVeiculoRepository
    {
        public MODELO_VEICULO GetItemById(Int32 id)
        {
            IQueryable<MODELO_VEICULO> query = Db.MODELO_VEICULO;
            query = query.Where(p => p.MOVE_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<MODELO_VEICULO> GetAllItens()
        {
            IQueryable<MODELO_VEICULO> query = Db.MODELO_VEICULO;
            return query.ToList();
        }
    }
}
 