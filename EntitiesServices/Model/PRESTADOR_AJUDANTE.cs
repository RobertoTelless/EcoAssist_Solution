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
    
    public partial class PRESTADOR_AJUDANTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRESTADOR_AJUDANTE()
        {
            this.ORDEM_SERVICO_PRESTADOR = new HashSet<ORDEM_SERVICO_PRESTADOR>();
            this.PRESTADOR_AJUDANTE_ANOTACOES = new HashSet<PRESTADOR_AJUDANTE_ANOTACOES>();
        }
    
        public int PRAJ_CD_ID { get; set; }
        public int PRES_CD_ID { get; set; }
        public string PRAJ_NM_NOME { get; set; }
        public string PRAJ_NR_RG { get; set; }
        public Nullable<System.DateTime> PRAJ_DT_RG_EMISSAO { get; set; }
        public string PRAJ_NM_RG_ORGAO_EMISSOR { get; set; }
        public int UF_CD_ID { get; set; }
        public string PRAJ_NR_CPF { get; set; }
        public string PARJ__AQ_FOTO { get; set; }
        public System.DateTime PRAJ_DT_CADASTRO { get; set; }
        public int PRAJ_IN_ATIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_PRESTADOR> ORDEM_SERVICO_PRESTADOR { get; set; }
        public virtual PRESTADOR PRESTADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_AJUDANTE_ANOTACOES> PRESTADOR_AJUDANTE_ANOTACOES { get; set; }
        public virtual UF UF { get; set; }
    }
}