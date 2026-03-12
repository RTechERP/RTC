using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class RequestInvoiceDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? STT { get; set; } // int, null
        public int? RequestInvoiceID { get; set; } // int, null
        public int? ProductSaleID { get; set; } // int, null
        public string ProductByProject { get; set; } // nvarchar(550), null
        public decimal? Quantity { get; set; } // decimal(18,2), null
        public int? ProjectID { get; set; } // int, null
        public int? POKHDetailID { get; set; } // int, null
        public string Specifications { get; set; } // nvarchar(550), null
        public string InvoiceNumber { get; set; } // varchar(150), null
        public DateTime? InvoiceDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string Note { get; set; } // datetime, null
        public bool? IsStock { get; set; } // bool, null
        public bool? IsDeleted { get; set; } // bool, null
    }
    public enum RequestInvoiceDetailModel_Enum
    {
        ID,
        STT,
        RequestInvoiceID,
        ProductSaleID,
        ProductByProject,
        Quantity,
        ProjectID,
        POKHDetailID,
        Specifications,
        InvoiceNumber,
        InvoiceDate,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        Note,
        IsStock,
        IsDeleted
    }
}
