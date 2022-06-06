using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;

namespace ERP_CRM_Solution.ViewModels
{
    public class PrestadorAjudanteAnotacoesViewModel
    {
        [Key]
        public int PRAA_CD_ID { get; set; }
        public int PRAJ_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo DATA obrigatorio")]
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public System.DateTime PRAA_DT_ANOTACAO { get; set; }
        public int USUA_CD_ID { get; set; }
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "A ANOTAÇÃO deve conter no minimo 1 caracteres e no máximo 5000.")]
        public string PRAA_TX_ANOTACAO { get; set; }

        public virtual PRESTADOR_AJUDANTE PRESTADOR_AJUDANTE { get; set; }
        public virtual USUARIO_SUGESTAO USUARIO_SUGESTAO { get; set; }

    }
}