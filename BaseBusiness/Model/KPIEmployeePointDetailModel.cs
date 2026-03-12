using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIEmployeePointDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? KPIEmployeePointID { get; set; } // int, null
        public int? KPIEvaluationRuleDetailID { get; set; } // int, null
        public decimal? FirstMonth { get; set; } // decimal(18,2), null
        public decimal? SecondMonth { get; set; } // decimal(18,2), null
        public decimal? ThirdMonth { get; set; } // decimal(18,2), null
        public decimal? PercentBonus { get; set; } // decimal(18,2), null
        public decimal? PercentRemaining { get; set; } // decimal(18,2), null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum KPIEmployeePointDetailModel_Enum{
        ID,
        KPIEmployeePointID,
        KPIEvaluationRuleDetailID,
        FirstMonth,
        SecondMonth,
        ThirdMonth,
        PercentBonus,
        PercentRemaining,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
