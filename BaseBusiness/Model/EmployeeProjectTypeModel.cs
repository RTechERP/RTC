using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class EmployeeProjectTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? EmployeeID { get; set; } // int, null
        public int? ProjectTypeID { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum EmployeeProjectTypeModel_Enum{
        ID,
        EmployeeID,
        ProjectTypeID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
