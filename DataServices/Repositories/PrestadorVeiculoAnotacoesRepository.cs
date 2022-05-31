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
    public class PrestadorVeiculoAnotacoesRepository : RepositoryBase<PRESTADOR_VEICULO_ANOTACOES>, IPrestadorVeiculoAnotacoesRepository
    {
        public List<PRESTADOR_VEICULO_ANOTACOES> GetAllItens()
        {
            return Db.PRESTADOR_VEICULO_ANOTACOES.ToList();
        }

        public PRESTADOR_VEICULO_ANOTACOES GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_VEICULO_ANOTACOES> query = Db.PRESTADOR_VEICULO_ANOTACOES.Where(p => p.PRVA_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 