using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace ERP_CRM_Solution.ViewModels
{
    public class PrestadorAjudanteViewModel
    {
        [Key]
        public int PRAJ_CD_ID { get; set; }
        public int PRES_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo NOME obrigatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "O NOME deve conter no minimo 1 caracteres e no máximo 50 caracteres.")]
        public string PRAJ_NM_NOME { get; set; }
        [StringLength(20, ErrorMessage = "O RG deve conter no máximo 20 caracteres.")]
        public string PRAJ_NR_RG { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public Nullable<System.DateTime> PRAJ_DT_RG_EMISSAO { get; set; }
        [StringLength(20, ErrorMessage = "O ÓRGÃO EMISSOR DO RG deve conter no máximo 20 caracteres.")]
        public string PRAJ_NM_RG_ORGAO_EMISSOR { get; set; }
        public int UF_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo CPF obrigatorio")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "O CPF deve conter no minimo 1 caracteres e no máximo 20 caracteres.")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        public string PRAJ_NR_CPF { get; set; }
        public string PARJ__AQ_FOTO { get; set; }
        public System.DateTime PRAJ_DT_CADASTRO { get; set; }
        public int PRAJ_IN_ATIVO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_PRESTADOR> ORDEM_SERVICO_PRESTADOR { get; set; }
        public virtual PRESTADOR PRESTADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_AJUDANTE_ANOTACOES> PRESTADOR_AJUDANTE_ANOTACOES { get; set; }
        public virtual UF UF { get; set; }
    }
}