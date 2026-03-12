using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectStatusLogModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int ProjectID { get; set; } // int, not null
        public int ProjectStatusID { get; set; } // int, not null
        public int EmployeeID { get; set; } // int, not null
        public DateTime DateLog { get; set; } // datetime, not null
        public DateTime CreatedDate { get; set; } // datetime, not null
        public DateTime UpdatedDate { get; set; } // datetime, not null
        public string CreatedBy { get; set; } // nvarchar(150), not null
        public string UpdatedBy { get; set; } // nvarchar(150), not null
    }
    public enum ProjectStatusLogModel_Enum{
        ID,
        ProjectID,
        ProjectStatusID,
        EmployeeID,
        DateLog,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        }
}
