using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace ERP_CRM_Solution.ViewModels
{
    public class PrestadorBancoViewModel
    {
        [Key]
        public int PRBA_CD_ID { get; set; }
        public int PRES_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo BANCO obrigatorio")]
        public int BANC_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo TIPO DE CONTA obrigatorio")]
        public int TICO_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo AGENCIA obrigatorio")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "A AGENCIA deve conter no minimo 1 caracteres e no máximo 10 caracteres.")]
        public string PRBA_NR_AGENCIA { get; set; }
        [Required(ErrorMessage = "Campo CONTA obrigatorio")]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "A CONTA deve conter no minimo 1 caracteres e no máximo 15 caracteres.")]
        public string PRBA_NR_CONTA { get; set; }
        public int PRBA_IN_ATIVO { get; set; }

        public virtual BANCO BANCO { get; set; }
        public virtual PRESTADOR PRESTADOR { get; set; }
        public virtual TIPO_CONTA TIPO_CONTA { get; set; }

    }
}