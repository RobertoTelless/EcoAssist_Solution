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
using CrossCutting;

namespace DataServices.Repositories
{
    public class PrestadorCnpjRepository : RepositoryBase<PRESTADOR_QUADRO_SOCIETARIO>, IPrestadorCnpjRepository
    {
        public PRESTADOR_QUADRO_SOCIETARIO CheckExist(PRESTADOR_QUADRO_SOCIETARIO cqs)
        {
            IQueryable<PRESTADOR_QUADRO_SOCIETARIO> query = Db.PRESTADOR_QUADRO_SOCIETARIO;
            query = query.Where(p => p.PRES_CD_ID == cqs.PRES_CD_ID && p.PRQS_NM_NOME == cqs.PRQS_NM_NOME);
            return query.FirstOrDefault();
        }

        public List<PRESTADOR_QUADRO_SOCIETARIO> GetAllItens()
        {
            IQueryable<PRESTADOR_QUADRO_SOCIETARIO> query = Db.PRESTADOR_QUADRO_SOCIETARIO;
            return query.ToList();
        }

        public List<PRESTADOR_QUADRO_SOCIETARIO> GetByPrestador(PRESTADOR item)
        {
            IQueryable<PRESTADOR_QUADRO_SOCIETARIO> query = Db.PRESTADOR_QUADRO_SOCIETARIO;
            query = query.Where(p => p.PRES_CD_ID == item.PRES_CD_ID);
             return query.ToList();
        }
    }
}