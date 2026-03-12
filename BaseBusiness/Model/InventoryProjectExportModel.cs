using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class InventoryProjectExportModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? InventoryProjectID { get; set; } // int, null
        public int? BillExportDetailID { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public decimal Quantity { get; set; } // nvarchar(150), null
    }
    public enum InventoryProjectExportModel_Enum{
        ID,
        InventoryProjectID,
        BillExportDetailID,
        IsDeleted,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        Quantity
    }
}
