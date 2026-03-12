using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class PONCCModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        /// <summary>
        /// trạng thái duyệt
        /// </summary>
        public bool? IsApproved { get; set; } // bit, null
        /// <summary>
        /// mã PO
        /// </summary>
        public string POCode { get; set; } // nvarchar(250), null
        /// <summary>
        /// nhà cc
        /// </summary>
        public string UserNCC { get; set; } // nvarchar(250), null
        /// <summary>
        /// mã hóa đơn
        /// </summary>
        public string BillCode { get; set; } // nvarchar(250), null
        /// <summary>
        /// ngày tạo PO
        /// </summary>
        public DateTime? ReceivedDatePO { get; set; } // datetime, null
        /// <summary>
        /// tổng tiền PO
        /// </summary>
        public decimal? TotalMoneyPO { get; set; } // decimal(18,2), null
        /// <summary>
        /// ngày yêu cầu giao hàng
        /// </summary>
        public DateTime? RequestDate { get; set; } // datetime, null
        /// <summary>
        /// người liên hệ
        /// </summary>
        public string UserName { get; set; } // nvarchar(250), null
        public string Phone { get; set; } // nvarchar(150), null
        public string Email { get; set; } // nvarchar(250), null
        public string GroupID { get; set; } // nvarchar(150), null
        public int? SupplierID { get; set; } // int, null
        public int? UserID { get; set; } // int, null
        public DateTime? DeliveryDate { get; set; } // datetime, null
        public DateTime? ExpectedDate { get; set; } // datetime, null
        /// <summary>
        /// Nhân viên mua hàng
        /// </summary>
        public int? EmployeeID { get; set; } // int, null
        /// <summary>
        /// Số ngày giao hàng
        /// </summary>
        public int? DeliveryTime { get; set; } // int, null
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; } // nvarchar(500), null
        /// <summary>
        /// Tình trạng đơn hàng(Đã giao ,Chưa giao,....)
        /// </summary>
        public int? Status { get; set; } // int, null
        /// <summary>
        /// Số tài khoản
        /// </summary>
        public string AccountNumber { get; set; } // nvarchar(50), null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? Status_Old { get; set; } // int, null
        public bool? NCCNew { get; set; } // bit, null
        public string RuleIncoterm { get; set; } // nvarchar(500), null
        public string RulePay { get; set; } // nvarchar(500), null
        public string BankingFee { get; set; } // nvarchar(500), null
        public string AddressDelivery { get; set; } // nvarchar(500), null
        public string SupplierVoucher { get; set; } // nvarchar(500), null
        public int? Company { get; set; } // int, null
        public string OriginItem { get; set; } // nvarchar(500), null
        public decimal? CurrencyRate { get; set; } // decimal(18,4), null
        public int? Currency { get; set; } // int, null
        public string FedexAccount { get; set; } // nvarchar(50), null
        public int? SupplierSaleID { get; set; } // int, null
        public bool? DeptSupplier { get; set; } // bit, null
        public string AccountNumberSupplier { get; set; } // nvarchar(550), null
        public string BankSupplier { get; set; } // nvarchar(550), null
        public string BankCharge { get; set; } // nvarchar(550), null
        public string OtherTerms { get; set; } // nvarchar(max), null
        public string OrderTargets { get; set; } // nvarchar(550), null
        public int? CurrencyID { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        /// <summary>
        /// 0: PO Thương mại; 1: PO mượn
        /// </summary>
        public int? POType { get; set; } // int, null
        public string ShippingPoint { get; set; } // nvarchar(550), null
        /// <summary>
        /// Không đạt chất lượng đơn hàng
        /// </summary>
        public bool? OrderQualityNotMet { get; set; } // bit, null
        /// <summary>
        /// Ngày tới hạn
        /// </summary>
        public string ReasonForFailure { get; set; } // nvarchar(max), null
    }
    public enum PONCCModel_Enum{
        ID,
        IsApproved,
        POCode,
        UserNCC,
        BillCode,
        ReceivedDatePO,
        TotalMoneyPO,
        RequestDate,
        UserName,
        Phone,
        Email,
        GroupID,
        SupplierID,
        UserID,
        DeliveryDate,
        ExpectedDate,
        EmployeeID,
        DeliveryTime,
        Note,
        Status,
        AccountNumber,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        Status_Old,
        NCCNew,
        RuleIncoterm,
        RulePay,
        BankingFee,
        AddressDelivery,
        SupplierVoucher,
        Company,
        OriginItem,
        CurrencyRate,
        Currency,
        FedexAccount,
        SupplierSaleID,
        DeptSupplier,
        AccountNumberSupplier,
        BankSupplier,
        BankCharge,
        OtherTerms,
        OrderTargets,
        CurrencyID,
        IsDeleted,
        POType,
        ShippingPoint,
        OrderQualityNotMet,
        ReasonForFailure,
        }
}
