using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class JobRequirementCheckPriceModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? JobRequirementID { get; set; } // int, null
        public DateTime? DeliveryDate { get; set; } // datetime, null
        public DateTime? RequestDate { get; set; } // datetime, null
        public string Customer { get; set; } // nvarchar(255), null
        public string ProductCode { get; set; } // nvarchar(255), null
        public int? Quantity { get; set; } // int, null
        public string Unit { get; set; } // nvarchar(50), null
        public string HRSuggestion { get; set; } // nvarchar(550), null
        public DateTime? ExpectedDate { get; set; } // datetime, null
        public string Note { get; set; } // nvarchar(550), null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
    }
    public enum JobRequirementCheckPriceModel_Enum{
        ID,
        JobRequirementID,
        DeliveryDate,
        RequestDate,
        Customer,
        ProductCode,
        Quantity,
        Unit,
        HRSuggestion,
        ExpectedDate,
        Note,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDeleted,
        }
}
