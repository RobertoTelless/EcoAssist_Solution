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
    
    public partial class CONTA_BANCO_ANEXO
    {
        public int CBAN_CD_ID { get; set; }
        public int COBA_CD_ID { get; set; }
        public System.DateTime CBAN_DT_ANEXO { get; set; }
        public string CBAN_NM_TITULO { get; set; }
        public int CBAN_IN_TIPO { get; set; }
        public string CBAN_AQ_ARQUIVO { get; set; }
        public int CBAN_IN_ATIVO { get; set; }
    
        public virtual CONTA_BANCO CONTA_BANCO { get; set; }
    }
}
