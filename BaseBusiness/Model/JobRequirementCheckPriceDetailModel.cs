using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class JobRequirementCheckPriceDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? JobRequirementCheckPriceID { get; set; } // int, null
        public decimal? OfferPrice { get; set; } // decimal(18,2), null
        public decimal? PurchasePrice { get; set; } // decimal(18,2), null
        public decimal? ShippingFee { get; set; } // decimal(18,2), null
        public decimal? TotalAmount { get; set; } // decimal(18,2), null
        public string LeadTime { get; set; } // nvarchar(255), null
        public int? VAT { get; set; } // int, null
        public string Supplier { get; set; } // nvarchar(550), null
        public int? Status { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
    }
    public enum JobRequirementCheckPriceDetailModel_Enum{
        ID,
        JobRequirementCheckPriceID,
        OfferPrice,
        PurchasePrice,
        ShippingFee,
        TotalAmount,
        LeadTime,
        VAT,
        Supplier,
        Status,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDeleted,
        }
}
