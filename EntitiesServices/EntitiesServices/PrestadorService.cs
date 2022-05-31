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
    public class PrestadorService : ServiceBase<PRESTADOR>, IPrestadorService
    {
        private readonly IPrestadorRepository _baseRepository;
        private readonly ILogRepository _logRepository;
        private readonly IUsuarioRepository _usuRepository;
        private readonly ITipoPessoaRepository _tpRepository;
        private readonly IBancoRepository _banRepository;
        private readonly IUFRepository _ufRepository;
        private readonly ITipoContaRepository _tcRepository;
        private readonly ITipoCertificadoRepository _tceRepository;
        private readonly ICategoriaCNHRepository _cnhRepository;
        private readonly IRegiaoRepository _regRepository;
        private readonly IRegiaoCoberturaRepository _rcRepository;
        private readonly ITipoVeiculoRepository _tvRepository;
        private readonly IMarcaVeiculoRepository _mvRepository;
        private readonly IModeloVeiculoRepository _mdRepository;
        private readonly ITipoCertificadoVeiculoRepository _tcvRepository;
        private readonly IVeiculoFuncaoRepository _vfRepository;
        private readonly IPrestadorAnexoRepository _anexoRepository;
        private readonly IPrestadorAnotacoesRepository _paRepository;

        private readonly IPrestadorContatoRepository _contRepository;
        private readonly IPrestadorBancoRepository _bancRepository;
        private readonly IPrestadorAjudanteRepository _ajudRepository;
        private readonly IPrestadorCertificadoRepository _certRepository;
        private readonly IPrestadorEnderecoRepository _endRepository;
        private readonly IPrestadorMotoristaRepository _motRepository;
        private readonly IPrestadorRegiaoRepository _regiRepository;
        private readonly IPrestadorVeiculoRepository _veicRepository;

        protected DB_EcoBaseEntities Db = new DB_EcoBaseEntities();

        public PrestadorService(IPrestadorRepository baseRepository, ILogRepository logRepository, IUsuarioRepository usuRepository, ITipoPessoaRepository tpRepository, IBancoRepository banRepository, IPrestadorAnexoRepository anexoRepository, IUFRepository ufRepository, ITipoContaRepository tcRepository, ITipoCertificadoRepository tceRepository, ICategoriaCNHRepository cnhRepository, IRegiaoRepository regRepository, IRegiaoCoberturaRepository rcRepository, ITipoVeiculoRepository tvRepository, IMarcaVeiculoRepository mvRepository, IModeloVeiculoRepository mdRepository, ITipoCertificadoVeiculoRepository tcvRepository, IVeiculoFuncaoRepository vfRepository, IPrestadorAnotacoesRepository paRepository, IPrestadorContatoRepository contRepository, IPrestadorBancoRepository bancRepository, IPrestadorAjudanteRepository ajuRepository, IPrestadorCertificadoRepository certRepository, IPrestadorEnderecoRepository endRepository, IPrestadorMotoristaRepository motRepository, IPrestadorRegiaoRepository regiRepository, IPrestadorVeiculoRepository veicRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
            _usuRepository = usuRepository;
            _anexoRepository = anexoRepository;
            _tpRepository = tpRepository;
            _banRepository = banRepository;
            _ufRepository = ufRepository;
            _tcRepository = tcRepository;
            _tceRepository = tceRepository;
            _cnhRepository = cnhRepository;
            _regRepository = regRepository;
            _rcRepository = rcRepository;
            _tvRepository = tvRepository;
            _mvRepository = mvRepository;
            _mdRepository = mdRepository;
            _tcvRepository = tcvRepository;
            _vfRepository = vfRepository;
            _paRepository = paRepository;
            _contRepository = contRepository;
            _bancRepository = bancRepository;
            _ajudRepository = ajuRepository;
            _certRepository = certRepository;
            _endRepository = endRepository;
            _motRepository = motRepository;
            _regiRepository = regiRepository;
            _veicRepository = veicRepository;
        }

        public PRESTADOR CheckExist(PRESTADOR conta)
        {
            PRESTADOR item = _baseRepository.CheckExist(conta);
            return item;
        }

        public List<UF> GetAllUF()
        {
            return _ufRepository.GetAllItens();
        }

        public UF GetUFbySigla(String sigla)
        {
            return _ufRepository.GetItemBySigla(sigla);
        }

        public PRESTADOR GetItemById(Int32 id)
        {
            PRESTADOR item = _baseRepository.GetItemById(id);
            return item;
        }

        public List<PRESTADOR> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<PRESTADOR> GetAllItensAdm()
        {
            return _baseRepository.GetAllItensAdm();
        }

        public List<USUARIO_SUGESTAO> GetAllUsuarios()
        {
            return _usuRepository.GetAllItens();
        }

        public List<BANCO> GetAllBanco()
        {
            return _banRepository.GetAllItens();
        }

        public List<TIPO_PESSOA> GetAllTiposPessoa()
        {
            return _tpRepository.GetAllItens();
        }

        public List<TIPO_CONTA> GetAllTipoContas()
        {
            return _tcRepository.GetAllItens();
        }

        public List<TIPO_CERTIFICADO> GetAllTipoCertificados()
        {
            return _tceRepository.GetAllItens();
        }

        public List<CNH_CATEGORIA> GetAllCatsCNH()
        {
            return _cnhRepository.GetAllItens();
        }

        public List<REGIAO> GetAllRegiao()
        {
            return _regRepository.GetAllItens();
        }

        public List<REGIAO_COBERTURA> GetAllRegiaoCobertura()
        {
            return _rcRepository.GetAllItens();
        }

        public List<TIPO_VEICULO> GetAllTipoVeiculo()
        {
            return _tvRepository.GetAllItens();
        }

        public List<MARCA_VEICULO> GetAllMarcaVeiculo()
        {
            return _mvRepository.GetAllItens();
        }

        public List<MODELO_VEICULO> GetAllModeloVeiculo()
        {
            return _mdRepository.GetAllItens();
        }

        public List<TIPO_CERTIFICADO_VEICULO> GetAllTipoCertificadoVeiculo()
        {
            return _tcvRepository.GetAllItens();
        }

        public List<VEICULO_FUNCAO> GetAllFuncaoVeiculo()
        {
            return _vfRepository.GetAllItens();
        }

        public PRESTADOR_ANEXO GetAnexoById(Int32 id)
        {
            return _anexoRepository.GetItemById(id);
        }

        public PRESTADOR_ANOTACOES GetComentarioById(Int32 id)
        {
            return _paRepository.GetItemById(id);
        }

        public List<PRESTADOR> ExecuteFilter(String nome, String razao, String cnpj, String email)
        {
            return _baseRepository.ExecuteFilter(nome, razao, cnpj, email);
        }

        public Int32 Create(PRESTADOR item, LOG log)
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

        public Int32 Create(PRESTADOR item)
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


        public Int32 Edit(PRESTADOR item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRESTADOR obj = _baseRepository.GetById(item.PRES_CD_ID);
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

        public Int32 Edit(PRESTADOR item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRESTADOR obj = _baseRepository.GetById(item.PRES_CD_ID);
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

        public Int32 Delete(PRESTADOR item, LOG log)
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

        // Contatos
        public PRESTADOR_CONTATO GetContatoById(Int32 id)
        {
            return _contRepository.GetItemById(id);
        }

        public Int32 EditContato(PRESTADOR_CONTATO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRESTADOR_CONTATO obj = _contRepository.GetById(item.PRCO_CD_ID);
                    _contRepository.Detach(obj);
                    _contRepository.Update(item);
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

        public Int32 CreateContato(PRESTADOR_CONTATO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _contRepository.Add(item);
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

        // Ajudantes
        public PRESTADOR_AJUDANTE GetAjudanteById(Int32 id)
        {
            return _ajudRepository.GetItemById(id);
        }

        public Int32 EditAjudante(PRESTADOR_AJUDANTE item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRESTADOR_AJUDANTE obj = _ajudRepository.GetById(item.PRAJ_CD_ID);
                    _ajudRepository.Detach(obj);
                    _ajudRepository.Update(item);
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

        public Int32 CreateAjudante(PRESTADOR_AJUDANTE item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _ajudRepository.Add(item);
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

        // Bancos
        public PRESTADOR_BANCO GetBancoById(Int32 id)
        {
            return _bancRepository.GetItemById(id);
        }

        public Int32 EditBanco(PRESTADOR_BANCO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRESTADOR_BANCO obj = _bancRepository.GetById(item.PRBA_CD_ID);
                    _bancRepository.Detach(obj);
                    _bancRepository.Update(item);
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

        public Int32 CreateBanco(PRESTADOR_BANCO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _bancRepository.Add(item);
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

        // Certificados
        public PRESTADOR_CERTIFICADO GetCertificadoById(Int32 id)
        {
            return _certRepository.GetItemById(id);
        }

        public Int32 EditCertificado(PRESTADOR_CERTIFICADO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRESTADOR_CERTIFICADO obj = _certRepository.GetById(item.PRCE_CD_ID);
                    _certRepository.Detach(obj);
                    _certRepository.Update(item);
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

        public Int32 CreateCertificado(PRESTADOR_CERTIFICADO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _certRepository.Add(item);
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

        // Enderecos
        public PRESTADOR_ENDERECO GetEnderecoById(Int32 id)
        {
            return _endRepository.GetItemById(id);
        }

        public Int32 EditEndereco(PRESTADOR_ENDERECO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRESTADOR_ENDERECO obj = _endRepository.GetById(item.PREN_CD_ID);
                    _endRepository.Detach(obj);
                    _endRepository.Update(item);
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

        public Int32 CreateEndereco(PRESTADOR_ENDERECO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _endRepository.Add(item);
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

        // Motorista
        public PRESTADOR_MOTORISTA GetMotoristaById(Int32 id)
        {
            return _motRepository.GetItemById(id);
        }

        public Int32 EditMotorista(PRESTADOR_MOTORISTA item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRESTADOR_MOTORISTA obj = _motRepository.GetById(item.PRMO_CD_ID);
                    _motRepository.Detach(obj);
                    _motRepository.Update(item);
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

        public Int32 CreateMotorista(PRESTADOR_MOTORISTA item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _motRepository.Add(item);
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

        // Regiao
        public PRESTADOR_REGIAO GetRegiaoById(Int32 id)
        {
            return _regiRepository.GetItemById(id);
        }

        public Int32 EditRegiao(PRESTADOR_REGIAO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRESTADOR_REGIAO obj = _regiRepository.GetById(item.PRRE_CD_ID);
                    _regiRepository.Detach(obj);
                    _regiRepository.Update(item);
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

        public Int32 CreateRegiao(PRESTADOR_REGIAO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _regiRepository.Add(item);
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

        // Veiculo
        public PRESTADOR_VEICULO GetVeiculoById(Int32 id)
        {
            return _veicRepository.GetItemById(id);
        }

        public Int32 EditVeiculo(PRESTADOR_VEICULO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRESTADOR_VEICULO obj = _veicRepository.GetById(item.PRVE_CD_ID);
                    _veicRepository.Detach(obj);
                    _veicRepository.Update(item);
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

        public Int32 CreateVeiculo(PRESTADOR_VEICULO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _veicRepository.Add(item);
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
