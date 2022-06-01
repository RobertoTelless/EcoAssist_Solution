using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace ERP_CRM_Solution.ViewModels
{
    public class PrestadorCertificadoViewModel
    {
        [Key]
        public int PRCE_CD_ID { get; set; }
        public int PRES_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo TIPO obrigatorio")]
        public int TICE_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo DATA EMISSÃO obrigatorio")]
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public System.DateTime PRDE_DT_EMISSAO { get; set; }
        [Required(ErrorMessage = "Campo DATA DE VALIDADE obrigatorio")]
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public System.DateTime PRCE_DT_VALIDADE { get; set; }
        public string PRCE_AQ_ARQUIVO { get; set; }
        public int PRCE_IN_ATIVO { get; set; }

        public virtual PRESTADOR PRESTADOR { get; set; }
        public virtual TIPO_CERTIFICADO TIPO_CERTIFICADO { get; set; }
    }
}