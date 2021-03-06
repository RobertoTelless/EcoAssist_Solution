using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IBeneficiarioAppService : IAppServiceBase<BENEFICIARIO>
    {
        Int32 ValidateCreate(BENEFICIARIO perfil, USUARIO usuario);
        Int32 ValidateEdit(BENEFICIARIO perfil, BENEFICIARIO perfilAntes, USUARIO usuario);
        Int32 ValidateEdit(BENEFICIARIO item, BENEFICIARIO itemAntes);
        Int32 ValidateDelete(BENEFICIARIO perfil, USUARIO usuario);
        Int32 ValidateReativar(BENEFICIARIO perfil, USUARIO usuario);

        BENEFICIARIO CheckExist(BENEFICIARIO conta);
        BENEFICIARIO GetItemById(Int32 id);
        List<BENEFICIARIO> GetAllItens();
        List<BENEFICIARIO> GetAllItensAdm();
        Int32 ExecuteFilter(Int32? tipo, Int32? sexo, Int32? estado, Int32? escolaridade, Int32? parentesco, String razao, String nome, DateTime? dataNasc, out List<BENEFICIARIO> objeto);

        List<ESCOLARIDADE> GetAllEscolaridade();
        List<PARENTESCO> GetAllParentesco();
        List<TIPO_PESSOA> GetAllTiposPessoa();
        List<UF> GetAllUF();
        UF GetUFbySigla(String sigla);
        List<SEXO> GetAllSexo();
        List<ESTADO_CIVIL> GetAllEstadoCivil();

        BENEFICIARIO_ANEXO GetAnexoById(Int32 id);
        BENEFICIARIO_ANOTACOES GetComentarioById(Int32 id);
    }
}
