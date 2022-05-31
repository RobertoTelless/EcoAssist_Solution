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
    public class PrestadorRepository : RepositoryBase<PRESTADOR>, IPrestadorRepository
    {
        public PRESTADOR CheckExist(PRESTADOR conta)
        {
            IQueryable<PRESTADOR> query = Db.PRESTADOR;
            query = query.Where(p => p.PRES_NR_CNPJ == conta.PRES_NR_CNPJ);
            return query.FirstOrDefault();
        }

        public PRESTADOR GetItemById(Int32 id)
        {
            IQueryable<PRESTADOR> query = Db.PRESTADOR;
            query = query.Where(p => p.PRES_CD_ID == id);
            query = query.Include(p => p.PRESTADOR_AJUDANTE);
            query = query.Include(p => p.PRESTADOR_ANEXO);
            query = query.Include(p => p.PRESTADOR_ANOTACOES);
            query = query.Include(p => p.PRESTADOR_BANCO);
            query = query.Include(p => p.PRESTADOR_CERTIFICADO);
            query = query.Include(p => p.PRESTADOR_CONTATO);
            query = query.Include(p => p.PRESTADOR_ENDERECO);
            query = query.Include(p => p.PRESTADOR_MOTORISTA);
            query = query.Include(p => p.PRESTADOR_REGIAO);
            query = query.Include(p => p.PRESTADOR_VEICULO);
            query = query.Include(p => p.ORDEM_SERVICO_PRESTADOR);
            return query.FirstOrDefault();
        }

        public List<PRESTADOR> GetAllItens()
        {
            IQueryable<PRESTADOR> query = Db.PRESTADOR.Where(p => p.PRES_IN_FLAG_ATIVO == 1);
            return query.ToList();
        }

        public List<PRESTADOR> GetAllItensAdm()
        {
            IQueryable<PRESTADOR> query = Db.PRESTADOR;
            return query.ToList();
        }

        public List<PRESTADOR> ExecuteFilter(String nome, String razao, String cnpj, String email)
        {
            List<PRESTADOR> lista = new List<PRESTADOR>();
            IQueryable<PRESTADOR> query = Db.PRESTADOR;
            if (!String.IsNullOrEmpty(razao))
            {
                query = query.Where(p => p.PRES_NM_RAZAO_SOCIAL.Contains(razao));
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.PRES_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(cnpj))
            {
                query = query.Where(p => p.PRES_NR_CNPJ == cnpj);
            }
            if (!String.IsNullOrEmpty(email))
            {
                query = query.Where(p => p.PRES_EM_EMAIL_PRINCIPAL.Contains(email));
            }
            if (query != null)
            {
                lista = query.ToList<PRESTADOR>();
            }
            return lista;
        }
    }
}
 