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
    public class PrestadorAppService : AppServiceBase<PRESTADOR>, IPrestadorAppService
    {
        private readonly IPrestadorService _baseService;
        private readonly IConfiguracaoService _confService;

        public PrestadorAppService(IPrestadorService baseService, IConfiguracaoService confService) : base(baseService)
        {
            _baseService = baseService;
            _confService = confService;
        }

        public List<PRESTADOR> GetAllItens()
        {
            List<PRESTADOR> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<PRESTADOR> GetAllItensAdm()
        {
            List<PRESTADOR> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public PRESTADOR GetItemById(Int32 id)
        {
            PRESTADOR item = _baseService.GetItemById(id);
            return item;
        }

        public PRESTADOR CheckExist(PRESTADOR conta)
        {
            PRESTADOR item = _baseService.CheckExist(conta);
            return item;
        }

        public List<USUARIO_SUGESTAO> GetAllUsuarios()
        {
            return _baseService.GetAllUsuarios();
        }

        public List<BANCO> GetAllBanco()
        {
            return _baseService.GetAllBanco();
        }

        public List<TIPO_PESSOA> GetAllTiposPessoa()
        {
            return _baseService.GetAllTiposPessoa();
        }

        public List<TIPO_CONTA> GetAllTipoContas()
        {
            return _baseService.GetAllTipoContas();
        }

        public List<TIPO_CERTIFICADO> GetAllTipoCertificados()
        {
            return _baseService.GetAllTipoCertificados();
        }

        public List<CNH_CATEGORIA> GetAllCatsCNH()
        {
            return _baseService.GetAllCatsCNH();
        }

        public List<REGIAO> GetAllRegiao()
        {
            return _baseService.GetAllRegiao();
        }

        public List<REGIAO_COBERTURA> GetAllRegiaoCobertura()
        {
            return _baseService.GetAllRegiaoCobertura();
        }

        public List<TIPO_VEICULO> GetAllTipoVeiculo()
        {
            return _baseService.GetAllTipoVeiculo();
        }

        public List<MARCA_VEICULO> GetAllMarcaVeiculo()
        {
            return _baseService.GetAllMarcaVeiculo();
        }

        public List<MODELO_VEICULO> GetAllModeloVeiculo()
        {
            return _baseService.GetAllModeloVeiculo();
        }

        public List<TIPO_CERTIFICADO_VEICULO> GetAllTipoCertificadoVeiculo()
        {
            return _baseService.GetAllTipoCertificadoVeiculo();
        }

        public List<VEICULO_FUNCAO> GetAllFuncaoVeiculo()
        {
            return _baseService.GetAllFuncaoVeiculo();
        }

        public List<UF> GetAllUF()
        {
            List<UF> lista = _baseService.GetAllUF();
            return lista;
        }

        public UF GetUFbySigla(String sigla)
        {
            return _baseService.GetUFbySigla(sigla);
        }

        public PRESTADOR_ANEXO GetAnexoById(Int32 id)
        {
            PRESTADOR_ANEXO lista = _baseService.GetAnexoById(id);
            return lista;
        }

        public PRESTADOR_ANOTACOES GetComentarioById(Int32 id)
        {
            return _baseService.GetComentarioById(id);
        }

        public Int32 ExecuteFilter(String nome, String razao, String cnpj, String email, out List<PRESTADOR> objeto)
        {
            try
            {
                objeto = new List<PRESTADOR>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(nome, razao, cnpj, email);
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

        public Int32 ValidateCreate(PRESTADOR item, USUARIO_SUGESTAO usuario)
        {
            try
            {
                var conf = _confService.GetItemById(1);

                // Completa objeto
                item.PRES_IN_FLAG_ATIVO = 1;


                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    LOG_NM_OPERACAO = "AddPRES",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<PRESTADOR>(item)
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

        public Int32 ValidateEdit(PRESTADOR item, PRESTADOR itemAntes, USUARIO_SUGESTAO usuario)
        {
            try
            {
                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    LOG_NM_OPERACAO = "EditPRES",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<PRESTADOR>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<PRESTADOR>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(PRESTADOR item, PRESTADOR itemAntes)
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

        public Int32 ValidateDelete(PRESTADOR item, USUARIO_SUGESTAO usuario)
        {
            try
            {
                // Verifica integridade referencial
                if (item.ORDEM_SERVICO_PRESTADOR.Count > 0)
                {
                    return 1;
                }

                // Acerta campos
                item.PRES_IN_FLAG_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelPRES",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<PRESTADOR>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(PRESTADOR item, USUARIO_SUGESTAO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.PRES_IN_FLAG_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatPRES",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<PRESTADOR>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Ajudante
        public PRESTADOR_AJUDANTE GetAjudanteById(Int32 id)
        {
            PRESTADOR_AJUDANTE lista = _baseService.GetAjudanteById(id);
            return lista;
        }

        public Int32 ValidateEditAjudante(PRESTADOR_AJUDANTE item)
        {
            try
            {
                // Persiste
                return _baseService.EditAjudante(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateAjudante(PRESTADOR_AJUDANTE item)
        {
            try
            {
                item.PRAJ_IN_ATIVO = 1;

                // Persiste
                Int32 volta = _baseService.CreateAjudante(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Bamco
        public PRESTADOR_BANCO GetBancoById(Int32 id)
        {
            PRESTADOR_BANCO lista = _baseService.GetBancoById(id);
            return lista;
        }

        public Int32 ValidateEditBanco(PRESTADOR_BANCO item)
        {
            try
            {
                // Persiste
                return _baseService.EditBanco(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateBanco(PRESTADOR_BANCO item)
        {
            try
            {
                item.PRBA_IN_ATIVO = 1;

                // Persiste
                Int32 volta = _baseService.CreateBanco(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Certificado
        public PRESTADOR_CERTIFICADO GetCertificadoById(Int32 id)
        {
            PRESTADOR_CERTIFICADO lista = _baseService.GetCertificadoById(id);
            return lista;
        }

        public Int32 ValidateEditCertificado(PRESTADOR_CERTIFICADO item)
        {
            try
            {
                // Persiste
                return _baseService.EditCertificado(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateCertificado(PRESTADOR_CERTIFICADO item)
        {
            try
            {
                item.PRCE_IN_ATIVO = 1;

                // Persiste
                Int32 volta = _baseService.CreateCertificado(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Endereco
        public PRESTADOR_ENDERECO GetEnderecoById(Int32 id)
        {
            PRESTADOR_ENDERECO lista = _baseService.GetEnderecoById(id);
            return lista;
        }

        public Int32 ValidateEditEndereco(PRESTADOR_ENDERECO item)
        {
            try
            {
                // Persiste
                return _baseService.EditEndereco(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateEndereco(PRESTADOR_ENDERECO item)
        {
            try
            {
                item.PREN_IN_ATIVO = 1;

                // Persiste
                Int32 volta = _baseService.CreateEndereco(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Contato
        public PRESTADOR_CONTATO GetContatoById(Int32 id)
        {
            PRESTADOR_CONTATO lista = _baseService.GetContatoById(id);
            return lista;
        }

        public Int32 ValidateEditContato(PRESTADOR_CONTATO item)
        {
            try
            {
                // Persiste
                return _baseService.EditContato(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateContato(PRESTADOR_CONTATO item)
        {
            try
            {
                item.PRCO_IN_ATIVO = 1;

                // Persiste
                Int32 volta = _baseService.CreateContato(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Motorista
        public PRESTADOR_MOTORISTA GetMotoristaById(Int32 id)
        {
            PRESTADOR_MOTORISTA lista = _baseService.GetMotoristaById(id);
            return lista;
        }

        public Int32 ValidateEditMotorista(PRESTADOR_MOTORISTA item)
        {
            try
            {
                // Persiste
                return _baseService.EditMotorista(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateMotorista(PRESTADOR_MOTORISTA item)
        {
            try
            {
                item.PRMO_IN_ATIVO = 1;

                // Persiste
                Int32 volta = _baseService.CreateMotorista(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Regiao
        public PRESTADOR_REGIAO GetRegiaoById(Int32 id)
        {
            PRESTADOR_REGIAO lista = _baseService.GetRegiaoById(id);
            return lista;
        }

        public Int32 ValidateEditRegiao(PRESTADOR_REGIAO item)
        {
            try
            {
                // Persiste
                return _baseService.EditRegiao(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateRegiao(PRESTADOR_REGIAO item)
        {
            try
            {
                item.PRRE_IN_ATIVO = 1;

                // Persiste
                Int32 volta = _baseService.CreateRegiao(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Veiculo
        public PRESTADOR_VEICULO GetVeiculoById(Int32 id)
        {
            PRESTADOR_VEICULO lista = _baseService.GetVeiculoById(id);
            return lista;
        }

        public Int32 ValidateEditVeiculo(PRESTADOR_VEICULO item)
        {
            try
            {
                // Persiste
                return _baseService.EditVeiculo(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateVeiculo(PRESTADOR_VEICULO item)
        {
            try
            {
                item.PRVE_IN_ATIVO = 1;

                // Persiste
                Int32 volta = _baseService.CreateVeiculo(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }





















    }
}
