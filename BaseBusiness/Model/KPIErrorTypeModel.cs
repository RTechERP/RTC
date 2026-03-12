using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIErrorTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? STT { get; set; } // int, null
        public string Code { get; set; } // nvarchar(max), null
        public string Name { get; set; } // nvarchar(max), null
        public bool? IsDelete { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum KPIErrorTypeModel_Enum{
        ID,
        STT,
        Code,
        Name,
        IsDelete,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
