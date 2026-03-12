using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectSurveyModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? STT { get; set; } // int, null
        public int? ProjectID { get; set; } // int, null
        /// <summary>
        /// Nhân viên Sale
        /// </summary>
        public int? EmployeeID { get; set; } // int, null
        public DateTime? DateStart { get; set; } // datetime, null
        public DateTime? DateEnd { get; set; } // datetime, null
        public string Address { get; set; } // nvarchar(550), null
        public string PIC { get; set; } // nvarchar(550), null
        public string Description { get; set; } // nvarchar(550), null
        public string Note { get; set; } // nvarchar(550), null
        public bool? IsUrgent { get; set; } // bit, null
        public bool? IsApprovedUrgent { get; set; } // bit, null
        public int? ApprovedUrgentID { get; set; } // int, null
        public string ReasonUrgent { get; set; } // nvarchar(550), null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string PhoneNumber { get; set; } // nvarchar(150), null
    }
    public enum ProjectSurveyModel_Enum{
        ID,
        STT,
        ProjectID,
        EmployeeID,
        DateStart,
        DateEnd,
        Address,
        PIC,
        Description,
        Note,
        IsUrgent,
        IsApprovedUrgent,
        ApprovedUrgentID,
        ReasonUrgent,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        PhoneNumber,
        }
}
