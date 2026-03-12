using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class MeetingMinutesDetailsModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? MeetingMinutesID { get; set; } // int, null
        public int? ProjectHistoryProblemID { get; set; } // int, null
        public int? EmployeeID { get; set; } // int, null
        public string CustomerName { get; set; } // nvarchar(250), null
        public string PhoneNumber { get; set; } // nvarchar(250), null
        public string DetailContent { get; set; } // nvarchar(max), null
        public string DetailResult { get; set; } // nvarchar(max), null
        public string Note { get; set; } // nvarchar(max), null
        public string DetailPlan { get; set; } // nvarchar(max), null
        public bool? IsEmployee { get; set; } // bit, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public bool? IsDeleted { get; set; } // bit, null
        public DateTime? PlanDate { get; set; } // bit, null
    }
    public enum MeetingMinutesDetailsModel_Enum{
        ID,
        MeetingMinutesID,
        ProjectHistoryProblemID,
        EmployeeID,
        CustomerName,
        PhoneNumber,
        DetailContent,
        DetailResult,
        Note,
        DetailPlan,
        IsEmployee,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        IsDeleted,
        PlanDate,
        }
}
