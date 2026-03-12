using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class InventoryStockModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? InventoryID { get; set; } // int, null
        public decimal? Quantity { get; set; } // decimal(18,2), null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        /// <summary>
        /// Người nhập số lượng tồn tối thiểu yêu cầu (Lưu ID của Employee)
        /// </summary>
        public int? EmployeeStock { get; set; } // int, null
        public int? ProductSaleID { get; set; } // int, null
        public int? WarehouseID { get; set; } // int, null
        public int? ProjectTypeID { get; set; } // int, null
        public int? EmployeeIDRequest { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public string Note { get; set; } // nvarchar(max), null
    }
    public enum InventoryStockModel_Enum
    {
        ID,
        InventoryID,
        Quantity,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        EmployeeStock,
        ProductSaleID,
        WarehouseID,
        ProjectTypeID,
        EmployeeIDRequest,
        IsDeleted,
        Note,
    }
}
