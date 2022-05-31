using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;

namespace DataServices.Repositories
{
    public class MarcaVeiculoRepository : RepositoryBase<MARCA_VEICULO>, IMarcaVeiculoRepository
    {
        public MARCA_VEICULO GetItemById(Int32 id)
        {
            IQueryable<MARCA_VEICULO> query = Db.MARCA_VEICULO;
            query = query.Where(p => p.MAVE_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<MARCA_VEICULO> GetAllItens()
        {
            IQueryable<MARCA_VEICULO> query = Db.MARCA_VEICULO;
            return query.ToList();
        }
    }
}
 