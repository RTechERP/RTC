using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectPartlistPurchaseRequestModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProjectPartListID { get; set; } // int, null
        public int? EmployeeID { get; set; } // int, null
        public string ProductCode { get; set; } // nvarchar(550), null
        public string ProductName { get; set; } // nvarchar(4000), null
        public int? UnitCountID { get; set; } // int, null
        /// <summary>
        /// 1:Y/c mua hàng;2:Huỷ Y/c mua; 3: Đã đặt hàng; 4: Đang về; 5:Đã về; 6:Không đặt hàng
        /// </summary>
        public int? StatusRequest { get; set; } // int, null
        public DateTime? DateRequest { get; set; } // datetime, null
        /// <summary>
        /// Ngày hàng về mong đợi (Deadline)
        /// </summary>
        public DateTime? DateReturnExpected { get; set; } // datetime, null
        public DateTime? DateOrder { get; set; } // datetime, null
        /// <summary>
        /// Ngày dự kiến hàng về
        /// </summary>
        public DateTime? DateEstimate { get; set; } // datetime, null
        /// <summary>
        /// Ngày hàng về thực tế
        /// </summary>
        public DateTime? DateReturnActual { get; set; } // datetime, null
        public DateTime? DateReceive { get; set; } // datetime, null
        public decimal? Quantity { get; set; } // decimal(18,2), null
        public decimal? UnitPrice { get; set; } // decimal(18,2), null
        public decimal? TotalPrice { get; set; } // decimal(18,2), null
        public string UnitMoney { get; set; } // nvarchar(50), null
        public int? SupplierSaleID { get; set; } // int, null
        public string Note { get; set; } // nvarchar(max), null
        public bool? IsApprovedTBP { get; set; } // bit, null
        public int? ApprovedTBP { get; set; } // int, null
        public bool? IsApprovedBGD { get; set; } // bit, null
        public int? ApprovedBGD { get; set; } // int, null
        public DateTime? DateApprovedTBP { get; set; } // datetime, null
        public DateTime? DateApprovedBGD { get; set; } // datetime, null
        public int? ProductSaleID { get; set; } // int, null
        public int? ProductGroupID { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? CurrencyID { get; set; } // int, null
        public decimal? CurrencyRate { get; set; } // decimal(18,2), null
        public decimal? HistoryPrice { get; set; } // decimal(18,2), null
        public decimal? TotalPriceExchange { get; set; } // decimal(18,2), null
        public string LeadTime { get; set; } // nvarchar(100), null
        public decimal? UnitFactoryExportPrice { get; set; } // decimal(18,2), null
        public decimal? UnitImportPrice { get; set; } // decimal(18,2), null
        public decimal? TotalImportPrice { get; set; } // decimal(18,2), null
        public bool? IsImport { get; set; } // bit, null
        public bool? IsRequestApproved { get; set; } // bit, null
        public int? EmployeeIDRequestApproved { get; set; } // int, null
        public string ReasonCancel { get; set; } // nvarchar(max), null
        public decimal? VAT { get; set; } // decimal(18,2), null
        public decimal? TotaMoneyVAT { get; set; } // decimal(18,2), null
        public int? TotalDayLeadTime { get; set; } // int, null
        public bool? IsCommercialProduct { get; set; } // bit, null
        public int? POKHDetailID { get; set; } // int, null
        public int? JobRequirementID { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public int? InventoryProjectID { get; set; } // int, null
        public bool? IsTechBought { get; set; } // bit, null
        public int? ProductGroupRTCID { get; set; } // int, null
        public int? ProductRTCID { get; set; } // int, null
        /// <summary>
        /// 0: yêu cầu mua; 1: Yêu cầu mượn
        /// </summary>
        public int? TicketType { get; set; } // int, null
        public DateTime? DateReturnEstimated { get; set; } // datetime, null
        public int? EmployeeApproveID { get; set; } // int, null
        public string NoteHR { get; set; } // nvarchar(550), null
        public string UnitName { get; set; } // nvarchar(150), null
        public string Maker { get; set; } // nvarchar(250), null
        /// <summary>
        /// 1: Mua dự án; 2: Kỹ thuật đã mua; 3: Mua demo; 4: Mượn demo;5: Hàng thương mại; 6: Hàng HR; 7: Hàng Marketing
        /// </summary>
        public int? ProjectPartlistPurchaseRequestTypeID { get; set; } // int, null
        public decimal? TargetPrice { get; set; } // decimal(18,2), null
        public int? DuplicateID { get; set; } // int, null
        public decimal? OriginQuantity { get; set; } // decimal(18,2), null
        public string ParentProductCode { get; set; } // decimal(18,2), null
        public bool? IsPurchase { get; set; } // decimal(18,2), null
    }
    public enum ProjectPartlistPurchaseRequestModel_Enum{
        ID,
        ProjectPartListID,
        EmployeeID,
        ProductCode,
        ProductName,
        UnitCountID,
        StatusRequest,
        DateRequest,
        DateReturnExpected,
        DateOrder,
        DateEstimate,
        DateReturnActual,
        DateReceive,
        Quantity,
        UnitPrice,
        TotalPrice,
        UnitMoney,
        SupplierSaleID,
        Note,
        IsApprovedTBP,
        ApprovedTBP,
        IsApprovedBGD,
        ApprovedBGD,
        DateApprovedTBP,
        DateApprovedBGD,
        ProductSaleID,
        ProductGroupID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        CurrencyID,
        CurrencyRate,
        HistoryPrice,
        TotalPriceExchange,
        LeadTime,
        UnitFactoryExportPrice,
        UnitImportPrice,
        TotalImportPrice,
        IsImport,
        IsRequestApproved,
        EmployeeIDRequestApproved,
        ReasonCancel,
        VAT,
        TotaMoneyVAT,
        TotalDayLeadTime,
        IsCommercialProduct,
        POKHDetailID,
        JobRequirementID,
        IsDeleted,
        InventoryProjectID,
        IsTechBought,
        ProductGroupRTCID,
        ProductRTCID,
        TicketType,
        DateReturnEstimated,
        EmployeeApproveID,
        NoteHR,
        UnitName,
        Maker,
        ProjectPartlistPurchaseRequestTypeID,
        TargetPrice,
        DuplicateID,
        OriginQuantity,
        ParentProductCode,
        IsPurchase
    }
}
