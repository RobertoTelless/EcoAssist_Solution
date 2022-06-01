using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ModelServices.Interfaces.Repositories;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Data.Entity;
using System.Data;

namespace ModelServices.EntitiesServices
{
    public class PrestadorCnpjService : ServiceBase<PRESTADOR_QUADRO_SOCIETARIO>, IPrestadorCnpjService
    {
        private readonly IPrestadorCnpjRepository _baseRepository;
        private readonly ILogRepository _logRepository;
        protected DB_EcoBaseEntities Db = new DB_EcoBaseEntities();

        public PrestadorCnpjService(IPrestadorCnpjRepository baseRepository, ILogRepository logRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
        }

        public PRESTADOR_QUADRO_SOCIETARIO CheckExist(PRESTADOR_QUADRO_SOCIETARIO cqs)
        {
            PRESTADOR_QUADRO_SOCIETARIO item = _baseRepository.CheckExist(cqs);
            return item;
        }

        public List<PRESTADOR_QUADRO_SOCIETARIO> GetAllItens()
        {
            List<PRESTADOR_QUADRO_SOCIETARIO> lista = _baseRepository.GetAllItens();
            return lista;
        }

        public List<PRESTADOR_QUADRO_SOCIETARIO> GetByPrestador(PRESTADOR item)
        {
            List<PRESTADOR_QUADRO_SOCIETARIO> lista = _baseRepository.GetByPrestador(item);
            return lista;
        }

        public Int32 Create(PRESTADOR_QUADRO_SOCIETARIO cqs, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _logRepository.Add(log);
                    _baseRepository.Add(cqs);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public Int32 Create(PRESTADOR_QUADRO_SOCIETARIO cqs)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _baseRepository.Add(cqs);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}