using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BillImportModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string BillImportCode { get; set; } // nvarchar(150), null
        public DateTime? CreatDate { get; set; } // datetime, null
        /// <summary>
        /// người giao
        /// </summary>
        public string Deliver { get; set; } // nvarchar(150), null
        /// <summary>
        /// Người nhận
        /// </summary>
        public string Reciver { get; set; } // nvarchar(150), null
        /// <summary>
        /// Trạng thái, 1:Duyệt, 0: Chưa duyệt
        /// </summary>
        public bool? Status { get; set; } // bit, null
        public string Suplier { get; set; } // nvarchar(150), null
        /// <summary>
        /// Loại phiếu: 1: Phiếu trả, 0: phiếu nhập bình thường
        /// </summary>
        public bool? BillType { get; set; } // bit, null
        /// <summary>
        /// Kho: 1:Kho sale, 2: kho dự án, 0: tất cả
        /// </summary>
        public string KhoType { get; set; } // nvarchar(150), null
        public string GroupID { get; set; } // nvarchar(150), null
        public int? SupplierID { get; set; } // int, null
        public int? DeliverID { get; set; } // int, null
        public int? ReciverID { get; set; } // int, null
        public int? KhoTypeID { get; set; } // int, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public int? UnApprove { get; set; } // int, null
        public bool? PTNB { get; set; } // bit, null
        public int? WarehouseID { get; set; } // int, null
        /// <summary>
        /// 0: Phiếu nhập;1: Phiếu trả;2: Phiếu trả nội bộ;3: Phiếu mượn NCC;4: Yêu cầu nhập kho

        /// </summary>
        public int? BillTypeNew { get; set; } // int, null
        /// <summary>
        /// 1:Hoàn thành; 2:Chưa hoàn thành
        /// </summary>
        public int? BillDocumentImportType { get; set; } // int, null
        public DateTime? DateRequestImport { get; set; } // datetime, null
        public int? RulePayID { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public int? BillExportID { get; set; } // int, null
        /// <summary>
        /// 1: đã đủ, 0: chưa đủ
        /// </summary>
        public bool? StatusDocumentImport { get; set; } // bit, null
    }
    public enum BillImportModel_Enum{
        ID,
        BillImportCode,
        CreatDate,
        Deliver,
        Reciver,
        Status,
        Suplier,
        BillType,
        KhoType,
        GroupID,
        SupplierID,
        DeliverID,
        ReciverID,
        KhoTypeID,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        UnApprove,
        PTNB,
        WarehouseID,
        BillTypeNew,
        BillDocumentImportType,
        DateRequestImport,
        RulePayID,
        IsDeleted,
        BillExportID,
        StatusDocumentImport,
        }
}
