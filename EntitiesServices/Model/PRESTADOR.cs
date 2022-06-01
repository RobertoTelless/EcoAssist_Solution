//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitiesServices.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRESTADOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRESTADOR()
        {
            this.CONTA_PAGAR = new HashSet<CONTA_PAGAR>();
            this.DESTINADOR_ENVIO = new HashSet<DESTINADOR_ENVIO>();
            this.ORDEM_SERVICO_PRESTADOR = new HashSet<ORDEM_SERVICO_PRESTADOR>();
            this.PRESTADOR_AJUDANTE = new HashSet<PRESTADOR_AJUDANTE>();
            this.PRESTADOR_ENDERECO = new HashSet<PRESTADOR_ENDERECO>();
            this.PRESTADOR_VEICULO = new HashSet<PRESTADOR_VEICULO>();
            this.PRESTADOR_ANEXO = new HashSet<PRESTADOR_ANEXO>();
            this.PRESTADOR_CERTIFICADO = new HashSet<PRESTADOR_CERTIFICADO>();
            this.PRESTADOR_REGIAO = new HashSet<PRESTADOR_REGIAO>();
            this.PRESTADOR_ANOTACOES = new HashSet<PRESTADOR_ANOTACOES>();
            this.PRESTADOR_CONTATO = new HashSet<PRESTADOR_CONTATO>();
            this.PRESTADOR_MOTORISTA = new HashSet<PRESTADOR_MOTORISTA>();
            this.PRESTADOR_BANCO = new HashSet<PRESTADOR_BANCO>();
            this.PRESTADOR_QUADRO_SOCIETARIO = new HashSet<PRESTADOR_QUADRO_SOCIETARIO>();
        }
    
        public int PRES_CD_ID { get; set; }
        public string PRES_NM_NOME { get; set; }
        public string PRES_NM_RAZAO_SOCIAL { get; set; }
        public string PRES_NR_CNPJ { get; set; }
        public string PRES_NM_RESPONSAVEL { get; set; }
        public string PRES_DS_CARGO { get; set; }
        public string PRES_EM_EMAIL_PRINCIPAL { get; set; }
        public string PRES_NR_TELEFONE_PRINCIPAL { get; set; }
        public string PRES_NR_CELULAR_PRINCIPAL { get; set; }
        public string PRES_NR_WHATSAPP_PRINCIPAL { get; set; }
        public string PRES_NM_WEBSITE { get; set; }
        public string PRES_NR_USUARIO_PRIME { get; set; }
        public int PRES_IN_POSSUI_SEGURO { get; set; }
        public int PRES_IN_UNIFORME_PROPRIO { get; set; }
        public int PRES_IN_UNIFORME_ECOASSIST { get; set; }
        public int PRES_NR_FUNCIONARIOS_CLT { get; set; }
        public int PRES_NR_FUNCIONARIOS_TERCEIROS { get; set; }
        public System.DateTime PRES_DT_INICIO_ATIVIDADE { get; set; }
        public int PRES_NR_PROPRIA_CAMINHAO { get; set; }
        public int PRES_NR_PROPRIA_UTILITARIO { get; set; }
        public int PRES_NR_PROPRIA_MOTO { get; set; }
        public int PRES_IN_PROPRIA_SEGURO { get; set; }
        public int PRES_NR_TERCEIRO_CAMINHAO { get; set; }
        public int PRES_NR_TERCEIRO_UTILITARIO { get; set; }
        public int PRES_NR_TERCEIRO_MOTO { get; set; }
        public int PRES_IN_TERCEIRO_SEGURO { get; set; }
        public int PRES_IN_RASTREAMENTO { get; set; }
        public System.DateTime PRES_DT_DATA_CADASTRO { get; set; }
        public byte[] PRES_TX_OBSERVACOES { get; set; }
        public int PRES_IN_FLAG_ATIVO { get; set; }
    
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
