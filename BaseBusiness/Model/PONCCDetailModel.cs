using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class PONCCDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? STT { get; set; } // int, null
        public int? PONCCID { get; set; } // int, null
        public int? ProductID { get; set; } // int, null
        public int? Qty { get; set; } // int, null
        public decimal? UnitPrice { get; set; } // decimal(18,2), null
        public decimal? IntoMoney { get; set; } // decimal(18,2), null
        public string CodeBill { get; set; } // nvarchar(250), null
        public string NameBill { get; set; } // nvarchar(250), null
        /// <summary>
        /// ngày yêu cầu giao hàng
        /// </summary>
        public DateTime? RequestDate { get; set; } // datetime, null
        /// <summary>
        /// ngày giao hàng thực tế
        /// </summary>
        public DateTime? ActualDate { get; set; } // datetime, null
        public int? RequestBuyRTCID { get; set; } // int, null
        public decimal? QtyRequest { get; set; } // decimal(18,2), null
        public decimal? QtyReal { get; set; } // decimal(18,2), null
        public decimal? Soluongcon { get; set; } // decimal(18,2), null
        public decimal? Price { get; set; } // decimal(18,2), null
        /// <summary>
        /// thuế giá trị gia tăng (%)
        /// </summary>
        public decimal? VAT { get; set; } // decimal(18,2), null
        public decimal? VATMoney { get; set; } // decimal(18,2), null
        public decimal? ThanhTien { get; set; } // decimal(18,2), null
        public decimal? TotalPrice { get; set; } // decimal(18,2), null
        /// <summary>
        /// ngày yêu cầu giao hàng
        /// </summary>
        public DateTime? OrderDate { get; set; } // datetime, null
        public DateTime? ExpectedDate { get; set; } // datetime, null
        public decimal? FeeShip { get; set; } // decimal(18,2), null
        public string Note { get; set; } // nvarchar(500), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // varchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(50), null
        public decimal? PriceSale { get; set; } // decimal(18,2), null
        public decimal? CurrencyExchange { get; set; } // decimal(18,2), null
        public decimal? Discount { get; set; } // decimal(18,2), null
        public decimal? ProfitRate { get; set; } // decimal(18,2), null
        public decimal? PriceHistory { get; set; } // decimal(18,2), null
        public string ProductCodeOfSupplier { get; set; } // nvarchar(500), null
        /// <summary>
        /// (0,chưa hoàn thành,1 hoàn thành)
        /// </summary>
        public int? Status { get; set; } // int, null
        public int? ProjectPartlistPurchaseRequestID { get; set; } // int, null
        public decimal? DiscountPercent { get; set; } // decimal(18,2), null
        public decimal? BiddingPrice { get; set; } // decimal(18,2), null
        public int? ProductSaleID { get; set; } // int, null
        public int? ProjectID { get; set; } // int, null
        public int? ProductRTCID { get; set; } // int, null
        public string ProjectName { get; set; } // nvarchar(max), null
        public DateTime? DeadlineDelivery { get; set; } // datetime, null
        public int? ProjectPartListID { get; set; } // int, null
        public bool? IsBill { get; set; } // bit, null
        public int? ProductType { get; set; } // int, null
        public DateTime? DateReturnEstimated { get; set; } // int, null
        public bool? IsStock { get; set; } // int, null
        public string UnitName { get; set; } // int, null
        public string ParentProductCode { get; set; } // int, null
        public bool IsPurchase { get; set; } // int, null
    }
    public enum PONCCDetailModel_Enum
    {
        ID,
        STT,
        PONCCID,
        ProductID,
        Qty,
        UnitPrice,
        IntoMoney,
        CodeBill,
        NameBill,
        RequestDate,
        ActualDate,
        RequestBuyRTCID,
        QtyRequest,
        QtyReal,
        Soluongcon,
        Price,
        VAT,
        VATMoney,
        ThanhTien,
        TotalPrice,
        OrderDate,
        ExpectedDate,
        FeeShip,
        Note,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        PriceSale,
        CurrencyExchange,
        Discount,
        ProfitRate,
        PriceHistory,
        ProductCodeOfSupplier,
        Status,
        ProjectPartlistPurchaseRequestID,
        DiscountPercent,
        BiddingPrice,
        ProductSaleID,
        ProjectID,
        ProductRTCID,
        ProjectName,
        DeadlineDelivery,
        ProjectPartListID,
        IsBill,
        ProductType,
        DateReturnEstimated,
        IsStock,
        UnitName,
        ParentProductCode,
        IsPurchase
    }
}
