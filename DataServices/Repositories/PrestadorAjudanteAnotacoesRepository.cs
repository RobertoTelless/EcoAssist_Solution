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
    public class PrestadorAjudanteAnotacoesRepository : RepositoryBase<PRESTADOR_AJUDANTE_ANOTACOES>, IPrestadorAjudanteAnotacoesRepository
    {
        public List<PRESTADOR_AJUDANTE_ANOTACOES> GetAllItens()
        {
            return Db.PRESTADOR_AJUDANTE_ANOTACOES.ToList();
        }

        public PRESTADOR_AJUDANTE_ANOTACOES GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR_AJUDANTE_ANOTACOES> query = Db.PRESTADOR_AJUDANTE_ANOTACOES.Where(p => p.PRAA_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
 