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
    
    public partial class TIPO_RESIDENCIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPO_RESIDENCIA()
        {
            this.CLIENTE_ENDERECO = new HashSet<CLIENTE_ENDERECO>();
            this.CLIENTE_PERGUNTA = new HashSet<CLIENTE_PERGUNTA>();
        }
    
        public int TIRE_CD_ID { get; set; }
        public string TIRE_NM_NOME { get; set; }
        public int TIRE_IN_ATIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENTE_ENDERECO> CLIENTE_ENDERECO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENTE_PERGUNTA> CLIENTE_PERGUNTA { get; set; }
    }
}
