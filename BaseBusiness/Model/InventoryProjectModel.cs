using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class InventoryProjectModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProjectID { get; set; } // int, null
        public int? ProductSaleID { get; set; } // int, null
        /// <summary>
        /// Người giữ
        /// </summary>
        public int? EmployeeID { get; set; } // int, null
        public int? WarehouseID { get; set; } // int, null
        public decimal? Quantity { get; set; } // decimal(18,2), null
        public bool? IsDeleted { get; set; } // bit, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public int? POKHDetailID { get; set; } // int, null
        public int? CustomerID { get; set; } // int, null
        public string Note { get; set; } // nvarchar(550), null
        public decimal? QuantityOrigin { get; set; } // decimal(18,2), null
        public int? ParentID { get; set; } // int, null
    }
    public enum InventoryProjectModel_Enum
    {
        ID,
        ProjectID,
        ProductSaleID,
        EmployeeID,
        WarehouseID,
        Quantity,
        IsDeleted,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        POKHDetailID,
        CustomerID,
        Note,
        QuantityOrigin,
        ParentID
    }
}
