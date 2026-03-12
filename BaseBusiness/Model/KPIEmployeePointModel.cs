using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIEmployeePointModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? EmployeeID { get; set; } // int, null
        public int? KPIEvaluationRuleID { get; set; } // int, null
        public int? Status { get; set; } // int, null
        public decimal? TotalPercent { get; set; } // decimal(18,2), null
        public bool? IsDelete { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsPublish { get; set; } // bit, null
        public decimal? TotalPercentActual { get; set; } // decimal(18,2), null
    }
    public enum KPIEmployeePointModel_Enum{
        ID,
        EmployeeID,
        KPIEvaluationRuleID,
        Status,
        TotalPercent,
        IsDelete,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsPublish,
        TotalPercentActual,
        }
}
