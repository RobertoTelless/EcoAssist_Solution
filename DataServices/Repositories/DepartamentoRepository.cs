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
    public class DepartamentoRepository : RepositoryBase<DEPARTAMENTO>, IDepartamentoRepository
    {
        public DEPARTAMENTO CheckExist(DEPARTAMENTO conta)
        {
            IQueryable<DEPARTAMENTO> query = Db.DEPARTAMENTO;
            query = query.Where(p => p.DEPT_NM_NOME == conta.DEPT_NM_NOME);
            return query.FirstOrDefault();
        }

        public DEPARTAMENTO GetItemById(Int32 id)
        {
            IQueryable<DEPARTAMENTO> query = Db.DEPARTAMENTO;
            query = query.Where(p => p.DEPT_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<DEPARTAMENTO> GetAllItensAdm()
        {
            IQueryable<DEPARTAMENTO> query = Db.DEPARTAMENTO;
            return query.ToList();
        }

        public List<DEPARTAMENTO> GetAllItens()
        {
            IQueryable<DEPARTAMENTO> query = Db.DEPARTAMENTO.Where(p => p.DEPT_IN_ATIVO == 1);
            return query.ToList();
        }

    }
}
 