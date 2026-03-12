using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class RequestInvoiceFileModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? RequestInvoiceID { get; set; } // int, null
        public string FileName { get; set; } // nvarchar(550), null
        public string OriginPath { get; set; } // nvarchar(max), null
        public string ServerPath { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? FileType { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
    }
    public enum RequestInvoiceFileModel_Enum{
        ID,
        RequestInvoiceID,
        FileName,
        OriginPath,
        ServerPath,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        FileType,
        IsDeleted,
        }
}
