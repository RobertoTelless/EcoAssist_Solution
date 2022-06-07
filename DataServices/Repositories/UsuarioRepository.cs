using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;
using CrossCutting;

namespace DataServices.Repositories
{
    public class UsuarioRepository : RepositoryBase<USUARIO_SUGESTAO>, IUsuarioRepository
    {
        public USUARIO_SUGESTAO GetByEmail(String email)
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_EM_EMAIL == email);
            return query.FirstOrDefault();
        }

        public USUARIO_SUGESTAO GetByEmailOnly(String email)
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_EM_EMAIL == email);
            return query.FirstOrDefault();
        }

        public USUARIO_SUGESTAO GetByLogin(String login)
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_NM_LOGIN == login);
            return query.FirstOrDefault();
        }

        public USUARIO_SUGESTAO GetItemById(Int32 id)
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_CD_ID == id);
            query = query.Include(p => p.PERFIL);
            query = query.Include(p => p.NOTIFICACAO);
            query = query.Include(p => p.LOG);
            return query.FirstOrDefault();
        }

        public USUARIO_SUGESTAO GetAdministrador()
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.PERFIL.PERF_SG_SIGLA == "ADM");
            query = query.Include(p => p.PERFIL);
            return query.FirstOrDefault();
        }

        public USUARIO_SUGESTAO GetComprador()
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_IN_COMPRADOR == 1);
            query = query.Include(p => p.PERFIL);
            return query.FirstOrDefault();
        }

        public USUARIO_SUGESTAO GetAprovador()
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_IN_APROVADOR == 1);
            query = query.Include(p => p.PERFIL);
            return query.FirstOrDefault();
        }

        public USUARIO_SUGESTAO GetTecnico()
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_IN_TECNICO.Value == 1);
            query = query.Include(p => p.PERFIL);
            return query.FirstOrDefault();
        }

        public List<USUARIO_SUGESTAO> GetAllItens()
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_IN_BLOQUEADO == 0);
            return query.ToList();
        }

        public List<USUARIO_SUGESTAO> GetAllTecnicos()
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_IN_TECNICO.Value == 1);
            return query.ToList();
        }

        public List<USUARIO_SUGESTAO> GetAllItensBloqueados()
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_IN_BLOQUEADO == 1);
            return query.ToList();
        }

        public List<USUARIO_SUGESTAO> GetAllItensAcessoHoje()
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_IN_BLOQUEADO == 0);
            query = query.Where(p => DbFunctions.TruncateTime(p.USUA_DT_ACESSO) == DbFunctions.TruncateTime(DateTime.Today.Date));
            return query.ToList();
        }

        public List<USUARIO_SUGESTAO> GetAllUsuariosAdm()
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO.Where(p => p.USUA_IN_ATIVO == 1);
            return query.ToList();
        }

        public List<USUARIO_SUGESTAO> GetAllUsuarios()
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO;
            return query.ToList();
        }

        public List<USUARIO_SUGESTAO> GetAllSistema()
        {
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO;
            query = query.Where(p => p.USUA_IN_SISTEMA == 1);
            return query.ToList();
        }

        public List<USUARIO_SUGESTAO> ExecuteFilter(Int32? perfilId, Int32? cargoId, String nome, String login, String email)
        {
            List<USUARIO_SUGESTAO> lista = new List<USUARIO_SUGESTAO>();
            IQueryable<USUARIO_SUGESTAO> query = Db.USUARIO_SUGESTAO;
            if (!String.IsNullOrEmpty(email))
            {
                query = query.Where(p => p.USUA_EM_EMAIL == email);
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.USUA_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(login))
            {
                query = query.Where(p => p.USUA_NM_LOGIN == login);
            }
            if (perfilId != 0)
            {
                query = query.Where(p => p.PERFIL.PERF_CD_ID == perfilId);
            }
            //if (cargoId != 0)
            //{
            //    query = query.Where(p => p.CARGO.CARG_CD_ID == cargoId);
            //}
            if (query != null)
            {
                query = query.OrderBy(a => a.USUA_NM_NOME);
                query = query.Include(p => p.PERFIL);
                lista = query.ToList<USUARIO_SUGESTAO>();
            }
            return lista;
        }
    }
}
