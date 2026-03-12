using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BillExportModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string Code { get; set; } // varchar(50), null
        public bool? TypeBill { get; set; } // bit, null
        public int? SupplierID { get; set; } // int, null
        public int? CustomerID { get; set; } // int, null
        public int? UserID { get; set; } // int, null
        public int? SenderID { get; set; } // int, null
        public int? StockID { get; set; } // int, null
        public string Description { get; set; } // nvarchar(100), null
        public string Address { get; set; } // nvarchar(250), null
        public DateTime? CreatDate { get; set; } // datetime, null
        public bool? IsApproved { get; set; } // bit, null
        /// <summary>
        /// 0. Mượn;1. Tồn kho;2. Đã xuất kho;3. Chia trước;4. Phiếu mượn nội bộ;5. Xuất trả NCC;6. Yêu cầu xuất kho
        /// </summary>
        public int? Status { get; set; } // int, null
        public string GroupID { get; set; } // nvarchar(50), null
        /// <summary>
        /// loại kho
        /// </summary>
        public string WarehouseType { get; set; } // nvarchar(250), null
        public int? KhoTypeID { get; set; } // int, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public int? ProductType { get; set; } // int, null
        public int? AddressStockID { get; set; } // int, null
        public bool? IsMerge { get; set; } // bit, null
        public int? UnApprove { get; set; } // int, null
        public int? WarehouseID { get; set; } // int, null
        public bool? IsPrepared { get; set; } // bit, null
        public bool? IsReceived { get; set; } // bit, null
        public DateTime? RequestDate { get; set; } // datetime, null
        public DateTime? PreparedDate { get; set; } // datetime, null
        public int? BillDocumentExportType { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public int? BillImportID { get; set; } // int, null
        public int? WareHouseTranferID { get; set; } // int, null
        public bool? IsTransfer { get; set; } // bit, null
    }
    public enum BillExportModel_Enum{
        ID,
        Code,
        TypeBill,
        SupplierID,
        CustomerID,
        UserID,
        SenderID,
        StockID,
        Description,
        Address,
        CreatDate,
        IsApproved,
        Status,
        GroupID,
        WarehouseType,
        KhoTypeID,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        ProductType,
        AddressStockID,
        IsMerge,
        UnApprove,
        WarehouseID,
        IsPrepared,
        IsReceived,
        RequestDate,
        PreparedDate,
        BillDocumentExportType,
        IsDeleted,
        BillImportID,
        WareHouseTranferID,
        IsTransfer,
        }
}
