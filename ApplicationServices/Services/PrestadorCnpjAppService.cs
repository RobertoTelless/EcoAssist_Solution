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
    public class PrestadorCnpjAppService : AppServiceBase<PRESTADOR_QUADRO_SOCIETARIO>, IPrestadorCnpjAppService
    {
        private readonly IPrestadorCnpjService _baseService;

        public PrestadorCnpjAppService(IPrestadorCnpjService baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        public PRESTADOR_QUADRO_SOCIETARIO CheckExist(PRESTADOR_QUADRO_SOCIETARIO cqs)
        {
            PRESTADOR_QUADRO_SOCIETARIO item = _baseService.CheckExist(cqs);
            return item;
        }

        public List<PRESTADOR_QUADRO_SOCIETARIO> GetAllItens()
        {
            List<PRESTADOR_QUADRO_SOCIETARIO> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<PRESTADOR_QUADRO_SOCIETARIO> GetByPrestador(PRESTADOR item)
        {
            List<PRESTADOR_QUADRO_SOCIETARIO> lista = _baseService.GetByPrestador(item);
            return lista;
        }

        public Int32 ValidateCreate(PRESTADOR_QUADRO_SOCIETARIO item, USUARIO_SUGESTAO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.CheckExist(item) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.PRQS_IN_ATIVO = 1;

                // Persiste
                Int32 volta = _baseService.Create(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}