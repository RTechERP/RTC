using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class RegisterIdeaModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? EmployeeID { get; set; } // int, null
        public DateTime? DateRegister { get; set; } // datetime, null
        public bool? IsApprovedTBP { get; set; } // bit, null
        public DateTime? DateApprovedTBP { get; set; } // datetime, null
        public int? ApprovedTBPID { get; set; } // int, null
        public bool? IsApproved { get; set; } // bit, null
        public DateTime? DateApproved { get; set; } // datetime, null
        public int? ApprovedID { get; set; } // int, null
        public string Note { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
        /// <summary>
        /// Lưu theo id bảng CourseCatalog (sửa lại)
        /// </summary>
        public int? RegisterIdeaTypeID { get; set; } // int, null
        public int? DepartmentOrganizationID { get; set; } // int, null
        public int? CourseID { get; set; } // int, null
    }
    public enum RegisterIdeaModel_Enum{
        ID,
        EmployeeID,
        DateRegister,
        IsApprovedTBP,
        DateApprovedTBP,
        ApprovedTBPID,
        IsApproved,
        DateApproved,
        ApprovedID,
        Note,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDeleted,
        RegisterIdeaTypeID,
        DepartmentOrganizationID,
        CourseID,
        }
}
