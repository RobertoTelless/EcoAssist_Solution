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
    
    public partial class PRESTADOR_QUADRO_SOCIETARIO
    {
        public int PRQS_CD_ID { get; set; }
        public int PRES_CD_ID { get; set; }
        public string PRQS_NM_QUALIFICACAO { get; set; }
        public string PRQS_NM_PAIS_ORIGEM { get; set; }
        public string PRQS_NM_REPRESENTANTE_LEGAL { get; set; }
        public string PRQS_NM_QUALIFICACAO_REP_LEGAL { get; set; }
        public string PRQS_NM_NOME { get; set; }
        public Nullable<int> PRQS_IN_ATIVO { get; set; }
    
        public virtual PRESTADOR PRESTADOR { get; set; }
    }
}