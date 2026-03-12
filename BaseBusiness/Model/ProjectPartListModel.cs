using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectPartListModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProjectID { get; set; } // int, null
        public int? STT { get; set; } // int, null
        public string GroupMaterial { get; set; } // nvarchar(max), null
        public string Model { get; set; } // nvarchar(max), null
        public string ProductCode { get; set; } // varchar(250), null
        public string Manufacturer { get; set; } // nvarchar(max), null
        public string Unit { get; set; } // nvarchar(50), null
        public decimal? QtyMin { get; set; } // decimal(18,2), null
        public decimal? QtyFull { get; set; } // decimal(18,2), null
        public decimal? Price { get; set; } // decimal(18,2), null
        public decimal? Amount { get; set; } // decimal(18,2), null
        public decimal? VAT { get; set; } // decimal(18,2), null
        public string LeadTime { get; set; } // nvarchar(100), null
        public DateTime? ExpectedReturnDate { get; set; } // datetime, null
        public int? Status { get; set; } // int, null
        public string Note { get; set; } // nvarchar(max), null
        public string Note1 { get; set; } // nvarchar(max), null
        public string Note2 { get; set; } // nvarchar(max), null
        public int? EmployeeID { get; set; } // int, null
        public string NCC { get; set; } // nvarchar(max), null
        public DateTime? RequestDate { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
        public decimal? QtyReturned { get; set; } // decimal(18,2), null
        public bool? IsApprovedTBP { get; set; } // bit, null
        public bool? IsApprovedPurchase { get; set; } // bit, null
        public int? ProjectPartListTypeID { get; set; } // int, null
        public int? ParentID { get; set; } // int, null
        public string Quality { get; set; } // nvarchar(max), null
        public DateTime? OrderDate { get; set; } // datetime, null
        public DateTime? ReturnDate { get; set; } // datetime, null
        public string TT { get; set; } // varchar(50), null
        public string OrderCode { get; set; } // varchar(150), null
        public string NCCFinal { get; set; } // nvarchar(750), null
        public decimal? PriceOrder { get; set; } // decimal(18,2), null
        public decimal? TotalPriceOrder { get; set; } // decimal(18,2), null
        public int? ProjectPartListVersionID { get; set; } // int, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public string SpecialCode { get; set; } // varchar(50), null
        public int? ProjectTypeID { get; set; } // int, null
        public int? StatusPriceRequest { get; set; } // int, null
        public DateTime? DeadlinePriceRequest { get; set; } // datetime, null
        public int? SupplierSaleID { get; set; } // int, null
        public string UnitMoney { get; set; } // nvarchar(max), null
        public DateTime? DatePriceRequest { get; set; } // datetime, null
        public DateTime? DatePriceQuote { get; set; } // datetime, null
        public decimal? QuantityReturn { get; set; } // decimal(18,2), null
        public string ReasonProblem { get; set; } // nvarchar(max), null
        public bool? IsProblem { get; set; } // bit, null
        public int? SuplierSaleFinalID { get; set; } // int, null
        public string LeadTimeRequest { get; set; } // nvarchar(max), null
        public string ReasonDeleted { get; set; } // nvarchar(max), null
        public bool? IsNewCode { get; set; } // bit, null
        public bool? IsApprovedTBPNewCode { get; set; } // bit, null
        public DateTime? DateApprovedNewCode { get; set; } // datetime, null
        public bool? IsStock { get; set; } // datetime, null
        public int? EmployeeApprovedNewCode { get; set; } // datetime, null
    }
    public enum ProjectPartListModel_Enum
    {
        ID,
        ProjectID,
        STT,
        GroupMaterial,
        Model,
        ProductCode,
        Manufacturer,
        Unit,
        QtyMin,
        QtyFull,
        Price,
        Amount,
        VAT,
        LeadTime,
        ExpectedReturnDate,
        Status,
        Note,
        Note1,
        Note2,
        EmployeeID,
        NCC,
        RequestDate,
        IsDeleted,
        QtyReturned,
        IsApprovedTBP,
        IsApprovedPurchase,
        ProjectPartListTypeID,
        ParentID,
        Quality,
        OrderDate,
        ReturnDate,
        TT,
        OrderCode,
        NCCFinal,
        PriceOrder,
        TotalPriceOrder,
        ProjectPartListVersionID,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        SpecialCode,
        ProjectTypeID,
        StatusPriceRequest,
        DeadlinePriceRequest,
        SupplierSaleID,
        UnitMoney,
        DatePriceRequest,
        DatePriceQuote,
        QuantityReturn,
        ReasonProblem,
        IsProblem,
        SuplierSaleFinalID,
        LeadTimeRequest,
        ReasonDeleted,
        IsNewCode,
        IsApprovedTBPNewCode,
        DateApprovedNewCode,
        IsStock,
        EmployeeApprovedNewCode
    }
}
