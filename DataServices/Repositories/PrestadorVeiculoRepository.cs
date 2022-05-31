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
    public class PrestadorVeiculoRepository : RepositoryBase<PRESTADOR_VEICULO>, IPrestadorVeiculoRepository
    {
        public List<PRESTADOR_VEICULO> GetAllItens()
        {
            return Db.PRESTADOR_VEICULO.ToList();
        }

        public PRESTADOR_VEICULO GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_VEICULO> query = Db.PRESTADOR_VEICULO.Where(p => p.PRVE_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 