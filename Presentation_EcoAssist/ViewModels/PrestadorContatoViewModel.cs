using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace ERP_CRM_Solution.ViewModels
{
    public class PrestadorContatoViewModel
    {
        [Key]
        public int PRCO_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo PRESTADOR obrigatorio")]
        public int PRES_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo NOME obrigatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "O NOME deve conter no minimo 1 caracteres e no máximo 50 caracteres.")]
        public string PRCO_NM_NOME { get; set; }
        [StringLength(50, ErrorMessage = "O CARGO deve conter no máximo 50 caracteres.")]
        public string PRCO_NM_CARGO { get; set; }
        [Required(ErrorMessage = "Campo E-MAIL obrigatorio")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O E-MAIL deve conter no minimo 1 caracteres e no máximo 100 caracteres.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Deve ser um e-mail válido")]
        public string PRCO_EM_EMAIL { get; set; }
        [StringLength(20, ErrorMessage = "O TELEFONE deve conter no máximo 20 caracteres.")]
        public string PRCO_NR_TELEFONE { get; set; }
        [StringLength(20, ErrorMessage = "O CELULAR deve conter no máximo 20 caracteres.")]
        public string PRCO_NR_CELULAR { get; set; }
        [StringLength(20, ErrorMessage = "O WHATSAPP deve conter no máximo 20 caracteres.")]
        public string PRCO_NR_WHATSAPP { get; set; }
        public int PRCO_IN_ATIVO { get; set; }

        public virtual PRESTADOR PRESTADOR { get; set; }
    }
}