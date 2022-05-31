using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IPrestadorService : IServiceBase<PRESTADOR>
    {
        Int32 Create(PRESTADOR perfil, LOG log);
        Int32 Create(PRESTADOR perfil);
        Int32 Edit(PRESTADOR perfil, LOG log);
        Int32 Edit(PRESTADOR perfil);
        Int32 Delete(PRESTADOR perfil, LOG log);

        PRESTADOR CheckExist(PRESTADOR conta);
        PRESTADOR GetItemById(Int32 id);
        List<PRESTADOR> GetAllItens();
        List<PRESTADOR> GetAllItensAdm();

        List<USUARIO_SUGESTAO> GetAllUsuarios();
        List<TIPO_PESSOA> GetAllTiposPessoa();
        List<UF> GetAllUF();
        UF GetUFbySigla(String sigla);
        List<BANCO> GetAllBanco();
        List<TIPO_CONTA> GetAllTipoContas();
        List<TIPO_CERTIFICADO> GetAllTipoCertificados();
        List<CNH_CATEGORIA> GetAllCatsCNH();
        List<REGIAO> GetAllRegiao();
        List<REGIAO_COBERTURA> GetAllRegiaoCobertura();
        List<TIPO_VEICULO> GetAllTipoVeiculo();
        List<MARCA_VEICULO> GetAllMarcaVeiculo();
        List<MODELO_VEICULO> GetAllModeloVeiculo();
        List<TIPO_CERTIFICADO_VEICULO> GetAllTipoCertificadoVeiculo();
        List<VEICULO_FUNCAO> GetAllFuncaoVeiculo();

        PRESTADOR_ANEXO GetAnexoById(Int32 id);
        PRESTADOR_ANOTACOES GetComentarioById(Int32 id);
        List<PRESTADOR> ExecuteFilter(String nome, String razao, String cnpj, String email);

        PRESTADOR_AJUDANTE GetAjudanteById(Int32 id);
        Int32 EditAjudante(PRESTADOR_AJUDANTE item);
        Int32 CreateAjudante(PRESTADOR_AJUDANTE item);

        PRESTADOR_BANCO GetBancoById(Int32 id);
        Int32 EditBanco(PRESTADOR_BANCO item);
        Int32 CreateBanco(PRESTADOR_BANCO item);

        PRESTADOR_CERTIFICADO GetCertificadoById(Int32 id);
        Int32 EditCertificado(PRESTADOR_CERTIFICADO item);
        Int32 CreateCertificado(PRESTADOR_CERTIFICADO item);

        PRESTADOR_CONTATO GetContatoById(Int32 id);
        Int32 EditContato(PRESTADOR_CONTATO item);
        Int32 CreateContato(PRESTADOR_CONTATO item);

        PRESTADOR_ENDERECO GetEnderecoById(Int32 id);
        Int32 EditEndereco(PRESTADOR_ENDERECO item);
        Int32 CreateEndereco(PRESTADOR_ENDERECO item);

        PRESTADOR_MOTORISTA GetMotoristaById(Int32 id);
        Int32 EditMotorista(PRESTADOR_MOTORISTA item);
        Int32 CreateMotorista(PRESTADOR_MOTORISTA item);

        PRESTADOR_REGIAO GetRegiaoById(Int32 id);
        Int32 EditRegiao(PRESTADOR_REGIAO item);
        Int32 CreateRegiao(PRESTADOR_REGIAO item);

        PRESTADOR_VEICULO GetVeiculoById(Int32 id);
        Int32 EditVeiculo(PRESTADOR_VEICULO item);
        Int32 CreateVeiculo(PRESTADOR_VEICULO item);

    }
}
