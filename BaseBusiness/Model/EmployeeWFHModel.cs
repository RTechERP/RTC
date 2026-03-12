using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class EmployeeWFHModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? EmployeeID { get; set; } // int, null
        public int? ApprovedID { get; set; } // int, null
        public bool? IsApproved { get; set; } // bit, null
        public string Reason { get; set; } // nvarchar(550), null
        public DateTime? DateWFH { get; set; } // datetime, null
        /// <summary>
        /// 1: Buổi sáng; 2:Buổi chiều, 3: Cả ngày
        /// </summary>
        public int? TimeWFH { get; set; } // int, null
        public string Note { get; set; } // nvarchar(550), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // varchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(50), null
        public decimal? TotalDay { get; set; } // decimal(18,2), null
        public int? ApprovedHR { get; set; } // int, null
        public bool? IsApprovedHR { get; set; } // bit, null
        /// <summary>
        /// 2: Không đồng ý duyệt; 1: Có đồng ý duyệt
        /// </summary>
        public int? DecilineApprove { get; set; } // int, null
        public string ReasonDeciline { get; set; } // nvarchar(max), null
        public string ReasonHREdit { get; set; } // nvarchar(max), null
        public bool? IsProblem { get; set; } // bit, null
        public string ContentWork { get; set; } // nvarchar(550), null
        public bool? IsApprovedBGD { get; set; } // bit, null
        public int? ApprovedBGDID { get; set; } // int, null
        public DateTime? DateApprovedBGD { get; set; } // datetime, null
        public string EvaluateResults { get; set; } // nvarchar(550), null
    }
    public enum EmployeeWFHModel_Enum{
        ID,
        EmployeeID,
        ApprovedID,
        IsApproved,
        Reason,
        DateWFH,
        TimeWFH,
        Note,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        TotalDay,
        ApprovedHR,
        IsApprovedHR,
        DecilineApprove,
        ReasonDeciline,
        ReasonHREdit,
        IsProblem,
        ContentWork,
        IsApprovedBGD,
        ApprovedBGDID,
        DateApprovedBGD,
        EvaluateResults,
        }
}
