using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BillImportDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        /// <summary>
        /// Mã master nhập
        /// </summary>
        public int? BillImportID { get; set; } // int, null
        public int? ProductID { get; set; } // int, null
        public decimal? Qty { get; set; } // decimal(18,2), null
        public decimal? Price { get; set; } // decimal(18,2), null
        public decimal? TotalPrice { get; set; } // decimal(18,2), null
        public string ProjectName { get; set; } // nvarchar(max), null
        public string ProjectCode { get; set; } // nvarchar(max), null
        /// <summary>
        /// sô hóa đơn
        /// </summary>
        public string SomeBill { get; set; } // nvarchar(550), null
        public string Note { get; set; } // nvarchar(max), null
        public int? STT { get; set; } // int, null
        public decimal? TotalQty { get; set; } // decimal(18,2), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public int? ProjectID { get; set; } // int, null
        public int? PONCCDetailID { get; set; } // int, null
        public string SerialNumber { get; set; } // nvarchar(50), null
        public string CodeMaPhieuMuon { get; set; } // nvarchar(50), null
        public int? BillExportDetailID { get; set; } // int, null
        public int? ProjectPartListID { get; set; } // int, null
        public bool? IsKeepProject { get; set; } // bit, null
        public decimal? QtyRequest { get; set; } // decimal(18,2), null
        public string BillCodePO { get; set; } // nvarchar(550), null
        /// <summary>
        /// 1: Đã trả, 2: Chưa trả
        /// </summary>
        public bool? ReturnedStatus { get; set; } // bit, null
        public int? InventoryProjectID { get; set; } // int, null
        public DateTime? DateSomeBill { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
        public string UnitName { get; set; } // nvarchar(150), null
        /// <summary>
        /// Số ngày công nợ
        /// </summary>
        public int? DPO { get; set; } // int, null
        /// <summary>
        /// Ngày tới hạn
        /// </summary>
        public DateTime? DueDate { get; set; } // datetime, null
        /// <summary>
        /// Tiền thuế giảm
        /// </summary>
        public decimal? TaxReduction { get; set; } // decimal(18,2), null
        /// <summary>
        /// Chi phí FE
        /// </summary>
        public decimal? COFormE { get; set; } // decimal(18,2), null

        /// <summary>
        /// 1: KHông giữ kho; 0: vẫn tự động giữ
        /// </summary>
        public bool? IsNotKeep { get; set; } // decimal(18,2), null
    }
    public enum BillImportDetailModel_Enum
    {
        ID,
        BillImportID,
        ProductID,
        Qty,
        Price,
        TotalPrice,
        ProjectName,
        ProjectCode,
        SomeBill,
        Note,
        STT,
        TotalQty,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        ProjectID,
        PONCCDetailID,
        SerialNumber,
        CodeMaPhieuMuon,
        BillExportDetailID,
        ProjectPartListID,
        IsKeepProject,
        QtyRequest,
        BillCodePO,
        ReturnedStatus,
        InventoryProjectID,
        DateSomeBill,
        IsDeleted,
        UnitName,
        DPO,
        DueDate,
        TaxReduction,
        COFormE,
        IsNotKeep
    }
}
