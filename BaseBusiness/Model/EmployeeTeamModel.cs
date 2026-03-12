using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class EmployeeTeamModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string Name { get; set; } // nvarchar(50), null
        public int? DepartmentID { get; set; } // int, null
        public string Code { get; set; } // varchar(50), null
        public int? STT { get; set; } // int, null
        public int? IsDeleted { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum EmployeeTeamModel_Enum{
        ID,
        Name,
        DepartmentID,
        Code,
        STT,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
