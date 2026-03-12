using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class InventoryProjectProductSaleLinkModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProductSaleID { get; set; } // int, null
        public int? EmployeeID { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
    }
    public enum InventoryProjectProductSaleLinkModel_Enum
    {
        ID,
        ProductSaleID,
        EmployeeID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsDeleted,
    }
}
