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
    
    public partial class PRESTADOR_VEICULO_CERTIFICADO
    {
        public int PRVC_CD_ID { get; set; }
        public int PRVE_CD_ID { get; set; }
        public int TICV_CD_ID { get; set; }
        public System.DateTime PRVC_DT_EMISSAO { get; set; }
        public System.DateTime PRVC_DT_VALIDADE { get; set; }
        public string PRVC_AQ_ARQUIVO { get; set; }
        public int PRVC_IN_ATIVO { get; set; }
    
        public virtual PRESTADOR_VEICULO PRESTADOR_VEICULO { get; set; }
        public virtual TIPO_CERTIFICADO_VEICULO TIPO_CERTIFICADO_VEICULO { get; set; }
    }
}