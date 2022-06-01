using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace ERP_CRM_Solution.ViewModels
{
    public class PrestadorMotoristaViewModel
    {
        [Key]
        public int PRMO_CD_ID { get; set; }
        public int PRES_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo NOME obrigatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "O NOME deve conter no minimo 1 caracteres e no máximo 50 caracteres.")]
        public string PRMO_NM_NOME { get; set; }
        [StringLength(20, ErrorMessage = "O RG deve conter no máximo 20 caracteres.")]
        public string PRMO_NR_RG { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public Nullable<System.DateTime> PRMO_DT_EMISSAO_RG { get; set; }
        [StringLength(20, ErrorMessage = "O ÓRGÃO EMISSOR DO RG deve conter no máximo 20 caracteres.")]
        public string PRMO_NM_ORGAO_RG { get; set; }
        public int UF_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo CPF obrigatorio")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "O CPF deve conter no minimo 1 caracteres e no máximo 20 caracteres.")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        public string PRMO_NR_CPF { get; set; }
        [Required(ErrorMessage = "Campo CNH obrigatorio")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "A CNH deve conter no minimo 1 caracteres e no máximo 20 caracteres.")]
        public string PRMO_NR_CNH { get; set; }
        [Required(ErrorMessage = "Campo VALIDADE DA CNH obrigatorio")]
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public Nullable<System.DateTime> PRMO_DT_CNH_VALIDADE { get; set; }
        [Required(ErrorMessage = "Campo CATEGORIA DA CNH obrigatorio")]
        public int CNCA_CD_ID { get; set; }
        public string PRMO_AQ_FOTO { get; set; }
        public System.DateTime PRMO_DT_CADASTRO { get; set; }
        public int PRMO_IN_ATIVO { get; set; }

        public virtual CNH_CATEGORIA CNH_CATEGORIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_PRESTADOR> ORDEM_SERVICO_PRESTADOR { get; set; }
        public virtual PRESTADOR PRESTADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_MOTORISTA_ANOTACOES> PRESTADOR_MOTORISTA_ANOTACOES { get; set; }
        public virtual UF UF { get; set; }
    }
}