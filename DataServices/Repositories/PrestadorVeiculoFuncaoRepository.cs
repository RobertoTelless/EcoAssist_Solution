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
    public class PrestadorVeiculoFuncaoRepository : RepositoryBase<PRESTADOR_VEICULO_FUNCAO>, IPrestadorVeiculoFuncaoRepository
    {
        public List<PRESTADOR_VEICULO_FUNCAO> GetAllItens()
        {
            return Db.PRESTADOR_VEICULO_FUNCAO.ToList();
        }

        public PRESTADOR_VEICULO_FUNCAO GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_VEICULO_FUNCAO> query = Db.PRESTADOR_VEICULO_FUNCAO.Where(p => p.PRVF_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 