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
    
    public partial class ORDEM_SERVICO_HISTORICO
    {
        public int OSHI_CD_ID { get; set; }
        public int ORSE_CD_ID { get; set; }
        public Nullable<System.DateTime> OSHI_DT_TROCA_STATUS { get; set; }
        public int OSHI_IN_NOVO_STATUS { get; set; }
        public byte[] OSHI_TX_OBSERVACOES { get; set; }
    
        public virtual ORDEM_SERVICO ORDEM_SERVICO { get; set; }
        public virtual STATUS_ORDEM_SERVICO STATUS_ORDEM_SERVICO { get; set; }
    }
}