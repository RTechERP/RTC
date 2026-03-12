using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class RequestInvoiceStatusModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string StatusCode { get; set; } // nvarchar(225), null
        public string StatusName { get; set; } // nvarchar(225), null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
    }
    public enum RequestInvoiceStatusModel_Enum{
        ID,
        StatusCode,
        StatusName,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDeleted,
        }
}
