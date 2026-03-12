using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class EmployeeTeamSaleModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string Name { get; set; } // nvarchar(150), null
        public int? STT { get; set; } // int, null
        public string Code { get; set; } // varchar(50), null
        public int? IsDeleted { get; set; } // int, null
        public int ParentID { get; set; } // int, not null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum EmployeeTeamSaleModel_Enum{
        ID,
        ParentID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        Name,
        STT,
        Code,
        IsDeleted,
        }
}
