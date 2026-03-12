using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class EmployeeTeamSaleLinkModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int EmployeeID { get; set; } // int, not null
        public int EmployeeTeamSaleID { get; set; } // int, not null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum EmployeeTeamSaleLinkModel_Enum{
        ID,
        EmployeeID,
        EmployeeTeamSaleID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
