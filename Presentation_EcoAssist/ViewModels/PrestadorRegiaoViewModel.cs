using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace ERP_CRM_Solution.ViewModels
{
    public class PrestadorRegiaoViewModel
    {
        [Key]
        public int PRRE_CD_ID { get; set; }
        public int PRES_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo REGIÃO obrigatorio")]
        public int REGI_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo REGIÃO - COBERTURA obrigatorio")]
        public int RECO_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo PREÇO BASE obrigatorio")]
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public decimal PRRE_VL_PRECO_BASE { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<decimal> PRRE_VL_PRECO_MAXIMO { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<decimal> PRRE_VL_PRECO_MINIMO { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<decimal> PRRE_ACRESCIMO_HORA { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<int> PRR_NR_DIAS_CARENCIA { get; set; }
        public int PRRE_IN_SEGUNDA { get; set; }
        public int PRRE_IN_TERCA { get; set; }
        public int PRRE_IN_QUARTA { get; set; }
        public int PRRE_IN_QUINTA { get; set; }
        public int PRRE_IN_SEXTA { get; set; }
        public int PRRE_IN_SABADO { get; set; }
        public Nullable<int> PRRE_IN_DOMINGO { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<int> PRRE_IN_SLA { get; set; }
        public Nullable<int> PRRE_IN_PRIORIDADE { get; set; }
        public int PRRE_IN_ATIVO { get; set; }

        public bool Segunda
        {
            get
            {
                if (PRRE_IN_SEGUNDA == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRRE_IN_SEGUNDA = (value == true) ? 1 : 0;
            }
        }
        public bool Terca
        {
            get
            {
                if (PRRE_IN_TERCA == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRRE_IN_TERCA = (value == true) ? 1 : 0;
            }
        }
        public bool Quarta
        {
            get
            {
                if (PRRE_IN_QUARTA == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRRE_IN_QUARTA = (value == true) ? 1 : 0;
            }
        }
        public bool Quinta
        {
            get
            {
                if (PRRE_IN_QUINTA == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRRE_IN_QUINTA = (value == true) ? 1 : 0;
            }
        }
        public bool Sexta
        {
            get
            {
                if (PRRE_IN_SEXTA == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRRE_IN_SEXTA = (value == true) ? 1 : 0;
            }
        }
        public bool Sabado
        {
            get
            {
                if (PRRE_IN_SABADO == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRRE_IN_SABADO = (value == true) ? 1 : 0;
            }
        }
        public bool Domingo
        {
            get
            {
                if (PRRE_IN_DOMINGO == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PRRE_IN_DOMINGO = (value == true) ? 1 : 0;
            }
        }

        public virtual PRESTADOR PRESTADOR { get; set; }
        public virtual REGIAO_COBERTURA REGIAO_COBERTURA { get; set; }
        public virtual REGIAO REGIAO { get; set; }
    }
}