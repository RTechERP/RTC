using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BillExportTechnicalModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string Code { get; set; } // varchar(100), null
        /// <summary>
        /// 0. Trả 1. Cho mượn;2. Tặng / Bán;3. Mất;4. Bảo hành;5. Xuất dự án;6. Hỏng;7. Xuất kho
        /// </summary>
        public int? BillType { get; set; } // int, null
        public int? CustomerID { get; set; } // int, null
        public string Receiver { get; set; } // nvarchar(100), null
        public string Deliver { get; set; } // nvarchar(100), null
        public string Addres { get; set; } // nvarchar(100), null
        public int? Status { get; set; } // int, null
        public string WarehouseType { get; set; } // nvarchar(100), null
        public string Note { get; set; } // nvarchar(250), null
        public string Image { get; set; } // nvarchar(100), null
        public int? ReceiverID { get; set; } // int, null
        public int? DeliverID { get; set; } // int, null
        public int? SupplierID { get; set; } // int, null
        public string CustomerName { get; set; } // nvarchar(100), null
        public string SupplierName { get; set; } // nvarchar(550), null
        public bool? CheckAddHistoryProductRTC { get; set; } // bit, null
        public DateTime? ExpectedDate { get; set; } // datetime, null
        public string ProjectName { get; set; } // nvarchar(550), null
        public int? WarehouseID { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? SupplierSaleID { get; set; } // int, null
        public int? BillDocumentExportType { get; set; } // int, null
        public int? ApproverID { get; set; } // int, null
    }
    public enum BillExportTechnicalModel_Enum
    {
        ID,
        Code,
        BillType,
        CustomerID,
        Receiver,
        Deliver,
        Addres,
        Status,
        WarehouseType,
        Note,
        Image,
        ReceiverID,
        DeliverID,
        SupplierID,
        CustomerName,
        SupplierName,
        CheckAddHistoryProductRTC,
        ExpectedDate,
        ProjectName,
        WarehouseID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        SupplierSaleID,
        BillDocumentExportType,
        ApproverID,
    }
}
