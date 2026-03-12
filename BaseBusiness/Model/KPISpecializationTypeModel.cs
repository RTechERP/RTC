using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPISpecializationTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? STT { get; set; } // int, null
        public int? DepartmentID { get; set; } // int, null
        public string Code { get; set; } // varchar(50), null
        public string Name { get; set; } // nvarchar(550), null
        public bool? IsDeleted { get; set; } // bit, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
    }
    public enum KPISpecializationTypeModel_Enum{
        ID,
        STT,
        DepartmentID,
        Code,
        Name,
        IsDeleted,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        }
}
