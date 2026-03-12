using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectPartListVersionModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProjectID { get; set; } // int, null
        public int? STT { get; set; } // int, null
        public string Code { get; set; } // varchar(50), null
        public string DescriptionVersion { get; set; } // nvarchar(max), null
        public bool? IsActive { get; set; } // bit, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public int? ProjectSolutionID { get; set; } // int, null
        public int? ProjectTypeID { get; set; } // int, null
        public int? StatusVersion { get; set; } // int, null
        public bool? IsApproved { get; set; } // bit, null
        public int? ApprovedID { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public string ReasonDeleted { get; set; } // nvarchar(550), null
    }
    public enum ProjectPartListVersionModel_Enum{
        ID,
        ProjectID,
        STT,
        Code,
        DescriptionVersion,
        IsActive,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        ProjectSolutionID,
        ProjectTypeID,
        StatusVersion,
        IsApproved,
        ApprovedID,
        IsDeleted,
        ReasonDeleted,
        }
}
