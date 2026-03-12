using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BillImportDetailTechnicalModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? STT { get; set; } // int, null
        public int? BillImportTechID { get; set; } // int, null
        public int? ProductID { get; set; } // int, null
        public decimal? Quantity { get; set; } // decimal(18,2), null
        public decimal? TotalQuantity { get; set; } // decimal(18,2), null
        public decimal? Price { get; set; } // decimal(18,2), null
        public decimal? TotalPrice { get; set; } // decimal(18,2), null
        public int? UnitID { get; set; } // int, null
        public string UnitName { get; set; } // nvarchar(100), null
        public int? ProjectID { get; set; } // int, null
        public string ProjectCode { get; set; } // nvarchar(100), null
        public string ProjectName { get; set; } // nvarchar(100), null
        public string SomeBill { get; set; } // nvarchar(250), null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string Note { get; set; } // nvarchar(max), null
        public string InternalCode { get; set; } // nvarchar(100), null
        public int? HistoryProductRTCID { get; set; } // int, null
        public int? ProductRTCQRCodeID { get; set; } // int, null
        public int? WarehouseID { get; set; } // int, null
        public int? IsBorrowSupplier { get; set; } // int, null
        public decimal? QtyRequest { get; set; } // decimal(18,2), null
        public int? PONCCDetailID { get; set; } // int, null
        public string BillCodePO { get; set; } // nvarchar(150), null
        public int? EmployeeIDBorrow { get; set; } // int, null
        public DateTime? DeadlineReturnNCC { get; set; } // datetime, null
        public DateTime? DateSomeBill { get; set; } // datetime, null
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
    }
    public enum BillImportDetailTechnicalModel_Enum
    {
        ID,
        STT,
        BillImportTechID,
        ProductID,
        Quantity,
        TotalQuantity,
        Price,
        TotalPrice,
        UnitID,
        UnitName,
        ProjectID,
        ProjectCode,
        ProjectName,
        SomeBill,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        Note,
        InternalCode,
        HistoryProductRTCID,
        ProductRTCQRCodeID,
        WarehouseID,
        IsBorrowSupplier,
        QtyRequest,
        PONCCDetailID,
        BillCodePO,
        EmployeeIDBorrow,
        DeadlineReturnNCC,
        DateSomeBill, 
        DPO,
        DueDate,
        TaxReduction,
        COFormE
    }
}
