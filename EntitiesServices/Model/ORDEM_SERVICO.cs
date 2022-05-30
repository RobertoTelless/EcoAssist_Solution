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
    
    public partial class ORDEM_SERVICO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDEM_SERVICO()
        {
            this.CLIENTE_HISTORICO = new HashSet<CLIENTE_HISTORICO>();
            this.CONTA_PAGAR = new HashSet<CONTA_PAGAR>();
            this.ORDEM_SERVICO_PROPOSTA = new HashSet<ORDEM_SERVICO_PROPOSTA>();
            this.ORDEM_SERVICO_PRODUTO = new HashSet<ORDEM_SERVICO_PRODUTO>();
            this.ORDEM_SERVICO_PRESTADOR = new HashSet<ORDEM_SERVICO_PRESTADOR>();
            this.ORDEM_SERVICO_HISTORICO = new HashSet<ORDEM_SERVICO_HISTORICO>();
            this.ORDEM_SERVICO_AGENDAMENTO_ANOTACAO = new HashSet<ORDEM_SERVICO_AGENDAMENTO_ANOTACAO>();
            this.ORDEM_SERVICO_ANEXO = new HashSet<ORDEM_SERVICO_ANEXO>();
            this.ORDEM_SERVICO_AGENDAMENTO = new HashSet<ORDEM_SERVICO_AGENDAMENTO>();
            this.ORDEM_SERVICO_ANOTACAO = new HashSet<ORDEM_SERVICO_ANOTACAO>();
            this.TICKET_ATENDIMENTO = new HashSet<TICKET_ATENDIMENTO>();
        }
    
        public int ORSE_CD_ID { get; set; }
        public int TIOS_CD_ID { get; set; }
        public int CLIE_CD_ID { get; set; }
        public int CLEN_CD_ID { get; set; }
        public int PARC_CD_ID { get; set; }
        public int STOS_CD_ID { get; set; }
        public string ORSE_NR_NUMERO { get; set; }
        public string ORSE_NR_INSTANCIA { get; set; }
        public System.DateTime ORSE_DT_CADASTRO { get; set; }
        public int ORSE_IN_CRIADA_POR { get; set; }
        public int ORSE_IN_APROVADA_POR { get; set; }
        public Nullable<System.DateTime> ORSE_DT_APROVADA_EM { get; set; }
        public string ORSE_NM_RETIRAR_COM { get; set; }
        public string ORSE_AQ_TEMO_DOACAO { get; set; }
        public byte[] ORSE_TX_OBSERVACOES { get; set; }
        public int ORSE_IN_ATIVO { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual CLIENTE_ENDERECO CLIENTE_ENDERECO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENTE_HISTORICO> CLIENTE_HISTORICO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTA_PAGAR> CONTA_PAGAR { get; set; }
        public virtual USUARIO_SUGESTAO USUARIO_SUGESTAO { get; set; }
        public virtual USUARIO_SUGESTAO USUARIO_SUGESTAO1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_PROPOSTA> ORDEM_SERVICO_PROPOSTA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_PRODUTO> ORDEM_SERVICO_PRODUTO { get; set; }
        public virtual STATUS_ORDEM_SERVICO STATUS_ORDEM_SERVICO { get; set; }
        public virtual PARCEIRO PARCEIRO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_PRESTADOR> ORDEM_SERVICO_PRESTADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_HISTORICO> ORDEM_SERVICO_HISTORICO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_AGENDAMENTO_ANOTACAO> ORDEM_SERVICO_AGENDAMENTO_ANOTACAO { get; set; }
        public virtual TIPO_ORDEM_SERVICO TIPO_ORDEM_SERVICO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_ANEXO> ORDEM_SERVICO_ANEXO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_AGENDAMENTO> ORDEM_SERVICO_AGENDAMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_ANOTACAO> ORDEM_SERVICO_ANOTACAO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TICKET_ATENDIMENTO> TICKET_ATENDIMENTO { get; set; }
    }
}