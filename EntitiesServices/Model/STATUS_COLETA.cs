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
    
    public partial class STATUS_COLETA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STATUS_COLETA()
        {
            this.ORDEM_SERVICO_AGENDAMENTO = new HashSet<ORDEM_SERVICO_AGENDAMENTO>();
            this.ORDEM_SERVICO_PRODUTO = new HashSet<ORDEM_SERVICO_PRODUTO>();
        }
    
        public int STCO_CD_ID { get; set; }
        public string STCO_NM_NOME { get; set; }
        public int STCO_IN_ATIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_AGENDAMENTO> ORDEM_SERVICO_AGENDAMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_PRODUTO> ORDEM_SERVICO_PRODUTO { get; set; }
    }
}
