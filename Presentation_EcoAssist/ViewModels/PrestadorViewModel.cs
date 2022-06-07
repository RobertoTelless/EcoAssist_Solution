using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace ERP_CRM_Solution.ViewModels
{
    public class PrestadorViewModel
    {
        [Key]
        public int PRES_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo NOME obrigatorio")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O NOME deve conter no minimo 1 caracteres e no máximo 100 caracteres.")]
        public string PRES_NM_NOME { get; set; }
        public string PRES_NM_RAZAO_SOCIAL { get; set; }
        [Required(ErrorMessage = "Campo CNPJ obrigatorio")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "O CNPJ deve conter no minimo 1 caracteres e no máximo 20 caracteres.")]
        [CustomValidationCNPJ(ErrorMessage = "CNPJ inválido")]
        public string PRES_NR_CNPJ { get; set; }
        [StringLength(50, ErrorMessage = "O RESPONSÁVEL deve conter no máximo 50 caracteres.")]
        public string PRES_NM_RESPONSAVEL { get; set; }
        [StringLength(50, ErrorMessage = "O CARGO deve conter no máximo 50 caracteres.")]
        public string PRES_DS_CARGO { get; set; }
        [StringLength(150, ErrorMessage = "O E-MAIL deve conter no máximo 150 caracteres.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Deve ser um e-mail válido")]
        public string PRES_EM_EMAIL_PRINCIPAL { get; set; }
        [StringLength(20, ErrorMessage = "O TELEFONE deve conter no máximo 20 caracteres.")]
        public string PRES_NR_TELEFONE_PRINCIPAL { get; set; }
        [StringLength(20, ErrorMessage = "O CELULAR deve conter no máximo 20 caracteres.")]
        public string PRES_NR_CELULAR_PRINCIPAL { get; set; }
        [StringLength(20, ErrorMessage = "O WHATSAPP deve conter no máximo 20 caracteres.")]
        public string PRES_NR_WHATSAPP_PRINCIPAL { get; set; }
        [StringLength(100, ErrorMessage = "O WEBSITE deve conter no máximo 100 caracteres.")]
        public string PRES_NM_WEBSITE { get; set; }
        public string PRES_NR_USUARIO_PRIME { get; set; }
        public int PRES_IN_POSSUI_SEGURO { get; set; }
        public int PRES_IN_UNIFORME_PROPRIO { get; set; }
        public int PRES_IN_UNIFORME_ECOASSIST { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public int PRES_NR_FUNCIONARIOS_CLT { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public int PRES_NR_FUNCIONARIOS_TERCEIROS { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public Nullable<System.DateTime> PRES_DT_INICIO_ATIVIDADE { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public int PRES_NR_PROPRIA_CAMINHAO { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public int PRES_NR_PROPRIA_UTILITARIO { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public int PRES_NR_PROPRIA_MOTO { get; set; }
        public int PRES_IN_PROPRIA_SEGURO { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public int PRES_NR_TERCEIRO_CAMINHAO { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public int PRES_NR_TERCEIRO_UTILITARIO { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public int PRES_NR_TERCEIRO_MOTO { get; set; }
        public int PRES_IN_TERCEIRO_SEGURO { get; set; }
        public int PRES_IN_RASTREAMENTO { get; set; }
        public System.DateTime PRES_DT_DATA_CADASTRO { get; set; }
        [StringLength(5000, ErrorMessage = "A OBSERVAÇÃO deve conter no máximo 5000 caracteres.")]
        public byte[] PRES_TX_OBSERVACOES { get; set; }
        public int PRES_IN_FLAG_ATIVO { get; set; }
        public string PRES_TX_OBSERVACAO { get; set; }

        public bool Seguro
        {
            get
            {
                if (PRES_IN_POSSUI_SEGURO == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRES_IN_POSSUI_SEGURO = (value == true) ? 1 : 0;
            }
        }
        public bool UniformeProprio
        {
            get
            {
                if (PRES_IN_UNIFORME_PROPRIO == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRES_IN_UNIFORME_PROPRIO = (value == true) ? 1 : 0;
            }
        }
        public bool UniformeEcoAssist
        {
            get
            {
                if (PRES_IN_UNIFORME_ECOASSIST == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRES_IN_UNIFORME_ECOASSIST = (value == true) ? 1 : 0;
            }
        }
        public bool SeguroProprio
        {
            get
            {
                if (PRES_IN_PROPRIA_SEGURO == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRES_IN_PROPRIA_SEGURO = (value == true) ? 1 : 0;
            }
        }
        public bool TerceiroSeguro
        {
            get
            {
                if (PRES_IN_TERCEIRO_SEGURO == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRES_IN_TERCEIRO_SEGURO = (value == true) ? 1 : 0;
            }
        }
        public bool Rastreamento
        {
            get
            {
                if (PRES_IN_RASTREAMENTO == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRES_IN_RASTREAMENTO = (value == true) ? 1 : 0;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTA_PAGAR> CONTA_PAGAR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESTINADOR_ENVIO> DESTINADOR_ENVIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_PRESTADOR> ORDEM_SERVICO_PRESTADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_AJUDANTE> PRESTADOR_AJUDANTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_ENDERECO> PRESTADOR_ENDERECO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_VEICULO> PRESTADOR_VEICULO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_ANEXO> PRESTADOR_ANEXO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_CERTIFICADO> PRESTADOR_CERTIFICADO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_REGIAO> PRESTADOR_REGIAO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_ANOTACOES> PRESTADOR_ANOTACOES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_CONTATO> PRESTADOR_CONTATO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_MOTORISTA> PRESTADOR_MOTORISTA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_BANCO> PRESTADOR_BANCO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_QUADRO_SOCIETARIO> PRESTADOR_QUADRO_SOCIETARIO { get; set; }

    }
}