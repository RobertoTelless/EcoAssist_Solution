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
    
    public partial class PRESTADOR_MOTORISTA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRESTADOR_MOTORISTA()
        {
            this.ORDEM_SERVICO_PRESTADOR = new HashSet<ORDEM_SERVICO_PRESTADOR>();
            this.PRESTADOR_MOTORISTA_ANOTACOES = new HashSet<PRESTADOR_MOTORISTA_ANOTACOES>();
        }
    
        public int PRMO_CD_ID { get; set; }
        public int PRES_CD_ID { get; set; }
        public string PRMO_NM_NOME { get; set; }
        public string PRMO_NR_RG { get; set; }
        public Nullable<System.DateTime> PRMO_DT_EMISSAO_RG { get; set; }
        public string PRMO_NM_ORGAO_RG { get; set; }
        public int UF_CD_ID { get; set; }
        public string PRMO_NR_CPF { get; set; }
        public string PRMO_NR_CNH { get; set; }
        public Nullable<System.DateTime> PRMO_DT_CNH_VALIDADE { get; set; }
        public int CNCA_CD_ID { get; set; }
        public string PRMO_AQ_FOTO { get; set; }
        public System.DateTime PRMO_DT_CADASTRO { get; set; }
        public int PRMO_IN_ATIVO { get; set; }
    
        public virtual CNH_CATEGORIA CNH_CATEGORIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_PRESTADOR> ORDEM_SERVICO_PRESTADOR { get; set; }
        public virtual PRESTADOR PRESTADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_MOTORISTA_ANOTACOES> PRESTADOR_MOTORISTA_ANOTACOES { get; set; }
        public virtual UF UF { get; set; }
    }
}
