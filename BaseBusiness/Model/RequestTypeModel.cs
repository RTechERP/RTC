using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class RequestTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string RequestTypeName { get; set; } // nvarchar(50), null
        public bool? IsIgnoreBGD { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(50), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum RequestTypeModel_Enum{
        ID,
        RequestTypeName,
        IsIgnoreBGD,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
