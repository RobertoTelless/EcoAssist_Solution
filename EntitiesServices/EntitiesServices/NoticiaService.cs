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
    public class NoticiaService : ServiceBase<NOTICIA>, INoticiaService
    {
        private readonly INoticiaRepository _baseRepository;
        private readonly ILogRepository _logRepository;
        private readonly INoticiaComentarioRepository _comRepository;
        protected DB_EcoBaseEntities Db = new DB_EcoBaseEntities();

        public NoticiaService(INoticiaRepository baseRepository, ILogRepository logRepository, INoticiaComentarioRepository comRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
            _comRepository = comRepository;
        }

        public NOTICIA GetItemById(Int32 id)
        {
            NOTICIA item = _baseRepository.GetItemById(id);
            return item;
        }

        public List<NOTICIA> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<NOTICIA> GetAllItensAdm()
        {
            return _baseRepository.GetAllItensAdm();
        }

        public List<NOTICIA> GetAllItensValidos()
        {
            return _baseRepository.GetAllItensValidos();
        }
        
        public NOTICIA_COMENTARIO GetComentarioById(Int32 id)
        {
            return _comRepository.GetItemById(id);
        }
       
        public List<NOTICIA> ExecuteFilter(String titulo, String autor, DateTime? data, String texto, String link)
        {
            return _baseRepository.ExecuteFilter(titulo, autor, data, texto, link);

        }

        public Int32 Create(NOTICIA item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _logRepository.Add(log);
                    _baseRepository.Add(item);
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

        public Int32 Create(NOTICIA item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _baseRepository.Add(item);
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


        public Int32 Edit(NOTICIA item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    NOTICIA obj = _baseRepository.GetById(item.NOTC_CD_ID);
                    _baseRepository.Detach(obj);
                    _logRepository.Add(log);
                    _baseRepository.Update(item);
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

        public Int32 Edit(NOTICIA item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    NOTICIA obj = _baseRepository.GetById(item.NOTC_CD_ID);
                    _baseRepository.Detach(obj);
                    _baseRepository.Update(item);
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

        public Int32 Delete(NOTICIA item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _logRepository.Add(log);
                    _baseRepository.Remove(item);
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
