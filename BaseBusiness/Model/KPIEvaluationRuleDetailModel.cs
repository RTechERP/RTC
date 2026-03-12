using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIEvaluationRuleDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string STT { get; set; } // nvarchar(150), null
        public int? KPIEvaluationID { get; set; } // int, null
        public int? KPIEvaluationRuleID { get; set; } // int, null
        public int? ParentID { get; set; } // int, null
        public string RuleContent { get; set; } // nvarchar(max), null
        public string FormulaCode { get; set; } // nvarchar(250), null
        public decimal? MaxPercent { get; set; } // decimal(18,2), null
        public decimal? PercentageAdjustment { get; set; } // decimal(18,2), null
        public decimal? MaxPercentageAdjustment { get; set; } // decimal(18,2), null
        public string RuleNote { get; set; } // nvarchar(max), null
        public string Note { get; set; } // nvarchar(max), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public bool? IsDeleted { get; set; } // bit, null
    }
    public enum KPIEvaluationRuleDetailModel_Enum{
        ID,
        STT,
        KPIEvaluationID,
        KPIEvaluationRuleID,
        ParentID,
        RuleContent,
        FormulaCode,
        MaxPercent,
        PercentageAdjustment,
        MaxPercentageAdjustment,
        RuleNote,
        Note,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        IsDeleted,
        }
}
