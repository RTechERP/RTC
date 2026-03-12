using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIEmployeeTeamModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string Name { get; set; } // nchar(200), null
        public int? DepartmentID { get; set; } // int, null
        public int? LeaderID { get; set; } // int, null
        public int? ParentID { get; set; } // int, null
        public int? YearValue { get; set; } // int, null
        public int? QuarterValue { get; set; } // int, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(50), null
        public bool? IsDeleted { get; set; } // bit, null
    }
    public enum KPIEmployeeTeamModel_Enum{
        ID,
        Name,
        DepartmentID,
        LeaderID,
        ParentID,
        YearValue,
        QuarterValue,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        IsDeleted,
        }
}
