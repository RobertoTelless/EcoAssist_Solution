using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace ERP_CRM_Solution.ViewModels
{
    public class PrestadorVeiculoViewModel
    {
        [Key]
        public int PRVE_CD_ID { get; set; }
        public int PRES_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo TIPO DE VEÍCULO obrigatorio")]
        public int TIVE_CD_ID { get; set; }
        public int MOVE_CD_ID { get; set; }
        public int MAVE_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo PLACA obrigatorio")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "A PLACA deve conter no minimo 1 caracteres e no máximo 10 caracteres.")]
        public string PRVE_NR_PLACA { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<int> PRVE_NR_CAPACIDADE { get; set; }
        public string PRVE_AQ_FOTO { get; set; }
        public int PRVE_IN_ATIVO { get; set; }

        public virtual MARCA_VEICULO MARCA_VEICULO { get; set; }
        public virtual MODELO_VEICULO MODELO_VEICULO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_PRESTADOR> ORDEM_SERVICO_PRESTADOR { get; set; }
        public virtual PRESTADOR PRESTADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_VEICULO_FUNCAO> PRESTADOR_VEICULO_FUNCAO { get; set; }
        public virtual TIPO_VEICULO TIPO_VEICULO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_VEICULO_ANOTACOES> PRESTADOR_VEICULO_ANOTACOES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_VEICULO_CERTIFICADO> PRESTADOR_VEICULO_CERTIFICADO { get; set; }
    }
}