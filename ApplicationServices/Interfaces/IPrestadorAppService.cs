using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IPrestadorAppService : IAppServiceBase<PRESTADOR>
    {
        Int32 ValidateCreate(PRESTADOR perfil, USUARIO_SUGESTAO usuario);
        Int32 ValidateEdit(PRESTADOR perfil, PRESTADOR perfilAntes, USUARIO_SUGESTAO usuario);
        Int32 ValidateEdit(PRESTADOR item, PRESTADOR itemAntes);
        Int32 ValidateDelete(PRESTADOR perfil, USUARIO_SUGESTAO usuario);
        Int32 ValidateReativar(PRESTADOR perfil, USUARIO_SUGESTAO usuario);

        PRESTADOR CheckExist(PRESTADOR conta);
        PRESTADOR GetItemById(Int32 id);
        List<PRESTADOR> GetAllItens();
        List<PRESTADOR> GetAllItensAdm();

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
        Int32 ExecuteFilter(String nome, String razao, String cnpj, String email, out List<PRESTADOR> objeto);

        PRESTADOR_AJUDANTE GetAjudanteById(Int32 id);
        Int32 ValidateEditAjudante(PRESTADOR_AJUDANTE item);
        Int32 ValidateCreateAjudante(PRESTADOR_AJUDANTE item);

        PRESTADOR_BANCO GetBancoById(Int32 id);
        Int32 ValidateEditBanco(PRESTADOR_BANCO item);
        Int32 ValidateCreateBanco(PRESTADOR_BANCO item);

        PRESTADOR_CERTIFICADO GetCertificadoById(Int32 id);
        Int32 ValidateEditCertificado(PRESTADOR_CERTIFICADO item);
        Int32 ValidateCreateCertificado(PRESTADOR_CERTIFICADO item);

        PRESTADOR_CONTATO GetContatoById(Int32 id);
        Int32 ValidateEditContato(PRESTADOR_CONTATO item);
        Int32 ValidateCreateContato(PRESTADOR_CONTATO item);

        PRESTADOR_ENDERECO GetEnderecoById(Int32 id);
        Int32 ValidateEditEndereco(PRESTADOR_ENDERECO item);
        Int32 ValidateCreateEndereco(PRESTADOR_ENDERECO item);

        PRESTADOR_MOTORISTA GetMotoristaById(Int32 id);
        Int32 ValidateEditMotorista(PRESTADOR_MOTORISTA item);
        Int32 ValidateCreateMotorista(PRESTADOR_MOTORISTA item);

        PRESTADOR_REGIAO GetRegiaoById(Int32 id);
        Int32 ValidateEditRegiao(PRESTADOR_REGIAO item);
        Int32 ValidateCreateRegiao(PRESTADOR_REGIAO item);

        PRESTADOR_VEICULO GetVeiculoById(Int32 id);
        Int32 ValidateEditVeiculo(PRESTADOR_VEICULO item);
        Int32 ValidateCreateVeiculo(PRESTADOR_VEICULO item);






    }
}
