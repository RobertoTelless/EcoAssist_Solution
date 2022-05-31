using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;

namespace DataServices.Repositories
{
    public class VeiculoFuncaoRepository : RepositoryBase<VEICULO_FUNCAO>, IVeiculoFuncaoRepository
    {
        public VEICULO_FUNCAO GetItemById(Int32 id)
        {
            IQueryable<VEICULO_FUNCAO> query = Db.VEICULO_FUNCAO;
            query = query.Where(p => p.VEFU_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<VEICULO_FUNCAO> GetAllItens()
        {
            IQueryable<VEICULO_FUNCAO> query = Db.VEICULO_FUNCAO;
            return query.ToList();
        }
    }
}
 