using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class BeneficiarioRepository : RepositoryBase<BENEFICIARIO>, IBeneficiarioRepository
    {
        public BENEFICIARIO CheckExist(BENEFICIARIO conta)
        {
            IQueryable<BENEFICIARIO> query = Db.BENEFICIARIO;
            query = query.Where(p => p.BENE_NM_NOME == conta.BENE_NM_NOME || p.MOME_NM_RAZAO_SOCIAL == conta.MOME_NM_RAZAO_SOCIAL);
            return query.FirstOrDefault();
        }

        public BENEFICIARIO GetItemById(Int32 id)
        {
            IQueryable<BENEFICIARIO> query = Db.BENEFICIARIO;
            query = query.Where(p => p.BENE_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<BENEFICIARIO> GetAllItens()
        {
            IQueryable<BENEFICIARIO> query = Db.BENEFICIARIO.Where(p => p.BENE_IN_ATIVO == 1);
            return query.ToList();
        }

        public List<BENEFICIARIO> GetAllItensAdm()
        {
            IQueryable<BENEFICIARIO> query = Db.BENEFICIARIO;
            return query.ToList();
        }

        public List<BENEFICIARIO> ExecuteFilter(Int32? tipo, Int32? sexo, Int32? estado, Int32? escolaridade, Int32? parentesco, String razao, String nome, DateTime? dataNasc)
        {
            List<BENEFICIARIO> lista = new List<BENEFICIARIO>();
            IQueryable<BENEFICIARIO> query = Db.BENEFICIARIO;
            if (tipo != null)
            {
                query = query.Where(p => p.TIPE_CD_ID == tipo);
            }
            if (sexo != null)
            {
                query = query.Where(p => p.SEXO_CD_ID == sexo);
            }
            if (estado != null)
            {
                query = query.Where(p => p.ESCI_CD_ID == estado);
            }
            if (escolaridade != null)
            {
                query = query.Where(p => p.ESCO_CD_ID == escolaridade);
            }
            if (parentesco != null)
            {
                query = query.Where(p => p.PARE_CD_ID == parentesco);
            }
            if (!String.IsNullOrEmpty(razao))
            {
                query = query.Where(p => p.MOME_NM_RAZAO_SOCIAL.Contains(razao));
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.BENE_NM_NOME.Contains(nome));
            }
            if (dataNasc != null)
            {
                query = query.Where(p => p.BENE_DT_NASCIMENTO == dataNasc);
            }
            if (query != null)
            {
                lista = query.ToList<BENEFICIARIO>();
            }
            return lista;
        }

    }
}
