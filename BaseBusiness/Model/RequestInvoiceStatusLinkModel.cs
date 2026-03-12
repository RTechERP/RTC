using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class RequestInvoiceStatusLinkModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? RequestInvoiceID { get; set; } // int, null
        public int? StatusID { get; set; } // int, null
        public int? IsApproved { get; set; } // int, null
        public bool? IsCurrent { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
        public string AmendReason { get; set; } // nvarchar(500), null
    }
    public enum RequestInvoiceStatusLinkModel_Enum{
        ID,
        RequestInvoiceID,
        StatusID,
        IsApproved,
        IsCurrent,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDeleted,
        AmendReason,
        }
}
