using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class CourseSpecializationLinkModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? CourseID { get; set; } // int, null
        public int? PositionTypeID { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(50), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
    }
    public enum CourseSpecializationLinkModel_Enum{
        ID,
        CourseID,
        PositionTypeID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDeleted,
        }
}
