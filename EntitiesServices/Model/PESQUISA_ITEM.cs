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
    
    public partial class PESQUISA_ITEM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PESQUISA_ITEM()
        {
            this.PESQUISA_ITEM_OPCAO = new HashSet<PESQUISA_ITEM_OPCAO>();
        }
    
        public int PEIT_CD_ID { get; set; }
        public int PESQ_CD_ID { get; set; }
        public int TIIT_CD_ID { get; set; }
        public string PEIT_DS_PERGUNTA { get; set; }
        public int PEIT_IN_ATIVO { get; set; }
    
        public virtual PESQUISA PESQUISA { get; set; }
        public virtual TIPO_ITEM_PESQUISA TIPO_ITEM_PESQUISA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PESQUISA_ITEM_OPCAO> PESQUISA_ITEM_OPCAO { get; set; }
    }
}
