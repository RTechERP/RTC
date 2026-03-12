using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class POKHHistoryModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string CustomerCode { get; set; } // nvarchar(250), null
        public string IndexCode { get; set; } // nvarchar(250), null
        public string PONumber { get; set; } // nvarchar(250), null
        public DateTime? PODate { get; set; } // datetime, null
        public string ProductCode { get; set; } // nvarchar(250), null
        public string Model { get; set; } // nvarchar(250), null
        public decimal? Quantity { get; set; } // decimal(18,2), null
        public decimal? QuantityDeliver { get; set; } // decimal(18,2), null
        public decimal? QuantityPending { get; set; } // decimal(18,2), null
        public string Unit { get; set; } // nvarchar(250), null
        public decimal? NetPrice { get; set; } // decimal(18,2), null
        public decimal? UnitPrice { get; set; } // decimal(18,2), null
        public decimal? TotalPrice { get; set; } // decimal(18,2), null
        public decimal? VAT { get; set; } // decimal(18,2), null
        public decimal? TotalPriceVAT { get; set; } // decimal(18,2), null
        public DateTime? DeliverDate { get; set; } // datetime, null
        public DateTime? PaymentDate { get; set; } // datetime, null
        public DateTime? BillDate { get; set; } // datetime, null
        public string BillNumber { get; set; } // nvarchar(250), null
        public string Dept { get; set; } // nvarchar(max), null
        public string Sale { get; set; } // nvarchar(max), null
        public string Pur { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string POTypeCode { get; set; } // nvarchar(150), null
    }
    public enum POKHHistoryModel_Enum{
        ID,
        CustomerCode,
        IndexCode,
        PONumber,
        PODate,
        ProductCode,
        Model,
        Quantity,
        QuantityDeliver,
        QuantityPending,
        Unit,
        NetPrice,
        UnitPrice,
        TotalPrice,
        VAT,
        TotalPriceVAT,
        DeliverDate,
        PaymentDate,
        BillDate,
        BillNumber,
        Dept,
        Sale,
        Pur,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        POTypeCode
    }
}
