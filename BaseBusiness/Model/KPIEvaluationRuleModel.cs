using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIEvaluationRuleModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? KPISessionID { get; set; } // int, null
        public int? KPIPositionID { get; set; } // int, null
        public string RuleCode { get; set; } // nvarchar(50), null
        public string RuleName { get; set; } // nvarchar(50), null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum KPIEvaluationRuleModel_Enum{
        ID,
        KPISessionID,
        KPIPositionID,
        RuleCode,
        RuleName,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
