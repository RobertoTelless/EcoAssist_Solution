using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ApplicationServices.Interfaces;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Text.RegularExpressions;

namespace ApplicationServices.Services
{
    public class AgendaAppService : AppServiceBase<AGENDA>, IAgendaAppService
    {
        private readonly IAgendaService _baseService;

        public AgendaAppService(IAgendaService baseService): base(baseService)
        {
            _baseService = baseService;
        }

        public List<AGENDA> GetAllItens()
        {
            List<AGENDA> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<AGENDA> GetAllItensAdm()
        {
            List<AGENDA> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public List<AGENDA> GetByDate(DateTime data)
        {
            List<AGENDA> lista = _baseService.GetByDate(data);
            return lista;
        }

        public List<AGENDA> GetByUser(Int32 id)
        {
            List<AGENDA> lista = _baseService.GetByUser(id);
            return lista;
        }

        public AGENDA GetItemById(Int32 id)
        {
            AGENDA item = _baseService.GetItemById(id);
            return item;
        }

        public List<CATEGORIA_AGENDA> GetAllTipos()
        {
            List<CATEGORIA_AGENDA> lista = _baseService.GetAllTipos();
            return lista;
        }

        public AGENDA_ANEXO GetAnexoById(Int32 id)
        {
            AGENDA_ANEXO lista = _baseService.GetAnexoById(id);
            return lista;
        }

        public Int32 ExecuteFilter(DateTime? data, Int32 ? cat, String titulo, String descricao, Int32 idUser, Int32 corp, out List<AGENDA> objeto)
        {
            try
            {
                objeto = new List<AGENDA>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(data, cat, titulo, descricao, idUser, corp);
                if (objeto.Count == 0)
                {
                    volta = 1;
                }
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreate(AGENDA item, USUARIO_SUGESTAO usuario)
        {
            try
            {
                // Verifica existencia pr??via
                // Completa objeto
                item.AGEN_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    LOG_NM_OPERACAO = "AddAGEN",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<AGENDA>(item)
                };

                // Persiste
                Int32 volta = _baseService.Create(item, log);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(AGENDA item, AGENDA itemAntes, USUARIO_SUGESTAO usuario)
        {
            try
            {
                // Persiste
                return _baseService.Edit(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDelete(AGENDA item, USUARIO_SUGESTAO usuario)
        {
            try
            {
                // Acerta campos
                item.AGEN_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelAGEN",
                    LOG_TX_REGISTRO = item.AGEN_CD_ID.ToString() + "|" + item.CATEGORIA_AGENDA.CAAG_NM_NOME + "|" + item.AGEN_DS_DESCRICAO + "|" + item.AGEN_DT_DATA.ToShortDateString() + "|" + item.AGEN_HR_HORA.ToString() + "|" + item.AGEN_NM_TITULO + "|" + item.USUARIO_SUGESTAO.USUA_NM_NOME + "|" + item.AGEN_TX_OBSERVACOES
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(AGENDA item, USUARIO_SUGESTAO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.AGEN_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatAGEN",
                    LOG_TX_REGISTRO = item.AGEN_CD_ID.ToString() + "|" + item.CATEGORIA_AGENDA.CAAG_NM_NOME + "|" + item.AGEN_DS_DESCRICAO + "|" + item.AGEN_DT_DATA.ToShortDateString() + "|" + item.AGEN_HR_HORA.ToString() + "|" + item.AGEN_NM_TITULO + "|" + item.USUARIO_SUGESTAO.USUA_NM_NOME + "|" + item.AGEN_TX_OBSERVACOES
            };

            // Persiste
            return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
