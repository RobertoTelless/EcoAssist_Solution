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
    
    public partial class CONTRATO_ANOTACOES
    {
        public int COAT_CD_ID { get; set; }
        public int CONT_CD_ID { get; set; }
        public int USUA_CD_ID { get; set; }
        public Nullable<System.DateTime> COAT_DT_ANOTACAO { get; set; }
        public string COAT_DS_ANOTACAO { get; set; }
    
        public virtual CONTRATO CONTRATO { get; set; }
        public virtual USUARIO_SUGESTAO USUARIO_SUGESTAO { get; set; }
    }
}
