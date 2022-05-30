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
    public class LogRepository : RepositoryBase<LOG>, ILogRepository
    {
        public LOG GetById(Int32 id)
        {
            IQueryable<LOG> query = Db.LOG.Where(p => p.LOG_CD_ID == id);
            query = query.Include(p => p.USUARIO_SUGESTAO);
            return query.FirstOrDefault();
        }

        public List<LOG> GetAllItens()
        {
            IQueryable<LOG> query = Db.LOG.Where(p => p.LOG_IN_ATIVO == 1);
            query = query.OrderByDescending(a => a.LOG_DT_DATA);
            return query.ToList();
        }

        public List<LOG> GetAllItensDataCorrente()
        {
            IQueryable<LOG> query = Db.LOG.Where(p => p.LOG_IN_ATIVO == 1);
            query = query.Where(p => DbFunctions.TruncateTime(p.LOG_DT_DATA) == DbFunctions.TruncateTime(DateTime.Today.Date));
            query = query.OrderByDescending(a => a.LOG_DT_DATA);
            return query.ToList();
        }

        public List<LOG> GetAllItensMesCorrente()
        {
            IQueryable<LOG> query = Db.LOG.Where(p => p.LOG_IN_ATIVO == 1);
            query = query.Where(p => DbFunctions.TruncateTime(p.LOG_DT_DATA).Value.Month == DbFunctions.TruncateTime(DateTime.Today.Date).Value.Month);
            query = query.OrderByDescending(a => a.LOG_DT_DATA);
            return query.ToList();
        }

        public List<LOG> GetAllItensMesAnterior()
        {
            var currentMonth = DateTime.Today.Month;
            var previousMonth = DateTime.Today.AddMonths(-1).Month;
            IQueryable<LOG> query = Db.LOG.Where(p => p.LOG_IN_ATIVO == 1);
            query = query.Where(p => DbFunctions.TruncateTime(p.LOG_DT_DATA).Value.Month == previousMonth);
            query = query.OrderByDescending(a => a.LOG_DT_DATA);
            return query.ToList();
        }

        public List<LOG> GetAllItensUsuario(Int32 id)
        {
            IQueryable<LOG> query = Db.LOG.Where(p => p.LOG_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_CD_ID == id);
            query = query.OrderByDescending(a => a.LOG_DT_DATA);
            return query.ToList();
        }

        public List<LOG> ExecuteFilter(Int32? usuId, DateTime? data, String operacao)
        {
            List<LOG> lista = new List<LOG>();
            IQueryable<LOG> query = Db.LOG;
            if (!String.IsNullOrEmpty(operacao))
            {
                query = query.Where(p => p.LOG_NM_OPERACAO == operacao);
            }
            if (usuId != 0)
            {
                query = query.Where(p => p.USUARIO_SUGESTAO.USUA_CD_ID == usuId);
            }
            if (data != null)
            {
                query = query.Where(p => DbFunctions.TruncateTime(p.LOG_DT_DATA) == DbFunctions.TruncateTime(data));
            }
            if (query != null)
            {
                query = query.OrderByDescending(a => a.LOG_DT_DATA);
                lista = query.ToList<LOG>();
            }
            return lista;
        }
    }
}
 