using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIEmployeeTeamLinkModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? EmployeeID { get; set; } // int, null
        public int? KPIEmployeeTeamID { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // varchar(20), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // varchar(20), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum KPIEmployeeTeamLinkModel_Enum{
        ID,
        EmployeeID,
        KPIEmployeeTeamID,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
