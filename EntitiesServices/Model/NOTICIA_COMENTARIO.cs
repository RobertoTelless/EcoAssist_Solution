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
    
    public partial class NOTICIA_COMENTARIO
    {
        public int NOCO_CD_ID { get; set; }
        public int NOTC_CD_ID { get; set; }
        public int USUA_CD_ID { get; set; }
        public Nullable<System.DateTime> NOCO_DT_COMENTARIO { get; set; }
        public string NOCO_DS_COMENTARIO { get; set; }
        public Nullable<int> NOCO_IN_ATIVO { get; set; }
    
        public virtual NOTICIA NOTICIA { get; set; }
        public virtual USUARIO_SUGESTAO USUARIO_SUGESTAO { get; set; }
    }
}
