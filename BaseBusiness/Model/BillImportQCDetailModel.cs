using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BillImportQCDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? BillImportQCID { get; set; } // int, null
        public int? ProductSaleID { get; set; } // int, null
        public int? LeaderTechID { get; set; } // int, null
        /// <summary>
        /// 1.OK 2.NG
        /// </summary>
        public int? Status { get; set; } // int, null
        public int? EmployeeTechID { get; set; } // int, null
        public string Note { get; set; } // nvarchar(max), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public int? BillImportDetailID { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public int? ProjectID { get; set; } // int, null
        public string POKHCode { get; set; } // nvarchar(550), null
        public decimal? Quantity { get; set; } // decimal(18,2), null
    }
    public enum BillImportQCDetailModel_Enum{
        ID,
        BillImportQCID,
        ProductSaleID,
        LeaderTechID,
        Status,
        EmployeeTechID,
        Note,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        BillImportDetailID,
        IsDeleted,
        ProjectID,
        POKHCode,
        Quantity,
        }
}
