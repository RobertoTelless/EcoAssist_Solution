using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;

namespace ERP_CRM_Solution.ViewModels
{
    public class TemplateSMSViewModel
    {
        [Key]
        public int TSMS_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo NOME obrigatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "O NOME deve conter no minimo 1 caracteres e no máximo 50 caracteres.")]
        public string TSMS_NM_NOME { get; set; }
        [Required(ErrorMessage = "Campo MENSAGEM obrigatorio")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "A MENSAGEM deve conter no minimo 1 caracteres e no máximo 250 caracteres.")]
        public string TSMS_TX_CORPO { get; set; }
        [StringLength(50, ErrorMessage = "O LINK deve conter máximo 50 caracteres.")]
        public string TSMS_LK_LINK { get; set; }
        public int TSMS_IN_ATIVO { get; set; }
        [Required(ErrorMessage = "Campo SIGLA obrigatorio")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "A SIGLA deve conter no minimo 1 caracteres e no máximo 10 caracteres.")]
        public string TSMS_SG_SIGLA { get; set; }

        public virtual ASSINANTE ASSINANTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MENSAGENS> MENSAGENS { get; set; }

    }
}