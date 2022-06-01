using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace ERP_CRM_Solution.ViewModels
{
    public class PrestadorEnderecoViewModel
    {
        [Key]
        public int PREN_CD_ID { get; set; }
        public int PRES_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo ENDERECO obrigatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "O ENDEREÇO deve conter no minimo 1 caracteres e no máximo 50 caracteres.")]
        public string PREN_NM_ENDERECO { get; set; }
        [Required(ErrorMessage = "Campo NÚMERO obrigatorio")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "O NÚMERO deve conter no minimo 1 caracteres e no máximo 10 caracteres.")]
        public string PRE_NR_NUMERO { get; set; }
        [StringLength(20, ErrorMessage = "O COMPLEMENTO deve conter no máximo 10 caracteres.")]
        public string PREN_NM_COMPLEMENTO { get; set; }
        [StringLength(50, ErrorMessage = "O BAIRRO deve conter no máximo 50 caracteres.")]
        public string PREN_NM_BAIRRO { get; set; }
        [StringLength(50, ErrorMessage = "A CIDADE deve conter no máximo 50 caracteres.")]
        public string PREN_NM_CIDADE { get; set; }
        [StringLength(10, ErrorMessage = "O CEP deve conter no máximo 10 caracteres.")]
        public string PREN_NR_CEP { get; set; }
        public int UF_CD_ID { get; set; }
        public int PREN_IN_FLAG_ESTOQUE { get; set; }
        [StringLength(50, ErrorMessage = "O NOME DO ESTOQUE deve conter no máximo 50 caracteres.")]
        public string PREN_NM_NOME_ESTOQUE { get; set; }
        public int PREN_IN_ATIVO { get; set; }
        [StringLength(150, ErrorMessage = "A REFERENCOA DE LOCAL deve conter no máximo 150 caracteres.")]
        public string PREN_DS_REFERENCIA_LOCAL { get; set; }

        public bool FlagEstoque
        {
            get
            {
                if (PREN_IN_FLAG_ESTOQUE == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PREN_IN_FLAG_ESTOQUE = (value == true) ? 1 : 0;
            }
        }

        public virtual PRESTADOR PRESTADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_ENDERECO_ESTOQUE> PRESTADOR_ENDERECO_ESTOQUE { get; set; }
        public virtual UF UF { get; set; }
    }
}