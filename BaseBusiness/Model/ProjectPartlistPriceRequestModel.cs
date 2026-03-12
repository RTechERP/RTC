using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectPartlistPriceRequestModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProjectPartListID { get; set; } // int, null
        public int? EmployeeID { get; set; } // int, null
        public string ProductCode { get; set; } // nvarchar(max), null
        public string ProductName { get; set; } // nvarchar(max), null
        /// <summary>
        /// 1:Yêu cầu báo giá; 2:Đã báo giá; 3:Hoàn thành
        /// </summary>
        public int? StatusRequest { get; set; } // int, null
        public DateTime? DateRequest { get; set; } // datetime, null
        public DateTime? Deadline { get; set; } // datetime, null
        public decimal? Quantity { get; set; } // decimal(18,2), null
        public decimal? UnitPrice { get; set; } // decimal(18,2), null
        public decimal? TotalPrice { get; set; } // decimal(18,2), null
        public string Unit { get; set; } // nvarchar(50), null
        public int? SupplierSaleID { get; set; } // int, null
        public string Note { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public DateTime? DatePriceQuote { get; set; } // datetime, null
        public decimal? TotalPriceExchange { get; set; } // decimal(18,2), null
        public decimal? CurrencyRate { get; set; } // decimal(18,2), null
        public int? CurrencyID { get; set; } // int, null
        public decimal? HistoryPrice { get; set; } // decimal(18,2), null
        public string LeadTime { get; set; } // nvarchar(100), null
        public decimal? UnitFactoryExportPrice { get; set; } // decimal(18,2), null
        public decimal? UnitImportPrice { get; set; } // decimal(18,2), null
        public decimal? TotalImportPrice { get; set; } // decimal(18,2), null
        public bool? IsImport { get; set; } // bit, null
        public bool? IsDeleted { get; set; } // bit, null
        public int? QuoteEmployeeID { get; set; } // int, null
        public bool? IsCheckPrice { get; set; } // bit, null
        public decimal? VAT { get; set; } // decimal(18,2), null
        public decimal? TotaMoneyVAT { get; set; } // decimal(18,2), null
        public int? TotalDayLeadTime { get; set; } // int, null
        public DateTime? DateExpected { get; set; } // datetime, null
        public bool? IsCommercialProduct { get; set; } // bit, null
        public int? POKHDetailID { get; set; } // int, null
        public string Maker { get; set; } // nvarchar(250), null
        public bool? IsJobRequirement { get; set; } // nvarchar(250), null
        public string NoteHR { get; set; } // nvarchar(250), null
        public int? JobRequirementID { get; set; } // nvarchar(250), null
        public bool? IsRequestBuy { get; set; } // nvarchar(250), null
        public int? ProjectPartlistPriceRequestTypeID { get; set; } // nvarchar(250), null
        public int? EmployeeIDUnPrice { get; set; } // int, null
        public string ReasonUnPrice { get; set; } // nvarchar(250), null
    }
    public enum ProjectPartlistPriceRequestModel_Enum
    {
        ID,
        ProjectPartListID,
        EmployeeID,
        ProductCode,
        ProductName,
        StatusRequest,
        DateRequest,
        Deadline,
        Quantity,
        UnitPrice,
        TotalPrice,
        Unit,
        SupplierSaleID,
        Note,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        DatePriceQuote,
        TotalPriceExchange,
        CurrencyRate,
        CurrencyID,
        HistoryPrice,
        LeadTime,
        UnitFactoryExportPrice,
        UnitImportPrice,
        TotalImportPrice,
        IsImport,
        IsDeleted,
        QuoteEmployeeID,
        IsCheckPrice,
        VAT,
        TotaMoneyVAT,
        TotalDayLeadTime,
        DateExpected,
        IsCommercialProduct,
        POKHDetailID,
        Maker,
        IsJobRequirement,
        NoteHR, 
        JobRequirementID,
        IsRequestBuy,
        ProjectPartlistPriceRequestTypeID,
        EmployeeIDUnPrice,
        ReasonUnPrice

    }
}
