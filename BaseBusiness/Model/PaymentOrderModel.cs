using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class PaymentOrderModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string Code { get; set; } // varchar(50), null
        /// <summary>
        /// Loại đề nghị (1:Đề nghị tạm ứng; 2:Đề nghị thanh toán/quyết toán)
        /// </summary>
        public int? TypeOrder { get; set; } // int, null
        public int? PaymentOrderTypeID { get; set; } // int, null
        /// <summary>
        /// Ngày làm đề nghị
        /// </summary>
        public DateTime? DateOrder { get; set; } // datetime, null
        public int? EmployeeID { get; set; } // int, null
        public string ReasonOrder { get; set; } // nvarchar(max), null
        /// <summary>
        /// Ngày quyết toán đối với đề nghị tạm ứng
        /// </summary>
        public DateTime? DatePayment { get; set; } // datetime, null
        public string ReceiverInfo { get; set; } // nvarchar(max), null
        /// <summary>
        /// Loại thanh toán(1:Chuyển khoản; 2:Tiền mặt)
        /// </summary>
        public int? TypePayment { get; set; } // int, null
        public string AccountNumber { get; set; } // nvarchar(max), null
        public string Bank { get; set; } // nvarchar(max), null
        public decimal? TotalMoney { get; set; } // decimal(18,2), null
        public string TotalMoneyText { get; set; } // nvarchar(max), null
        public string Unit { get; set; } // nvarchar(50), null
        public bool? IsDelete { get; set; } // bit, null
        public string Note { get; set; } // nvarchar(max), null
        /// <summary>
        /// 1:Chuyển khoản RTC; 2:Chuyển khoản MVI;3:Chuyển khoản APR;4:Chuyển khoản Yonko;5:Chuyển khoản cá nhân
        /// </summary>
        public int? TypeBankTransfer { get; set; } // int, null
        public string ContentBankTransfer { get; set; } // nvarchar(max), null
        public string AccountingNote { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public DateTime? DeadlinePayment { get; set; } // datetime, null
        public bool? IsUrgent { get; set; } // bit, null
        public int? PONCCID { get; set; } // int, null
        public int? SupplierSaleID { get; set; } // int, null
        public int? CustomerID { get; set; } // int, null
        public int? TypeDocument { get; set; } // int, null
        public string NumberDocument { get; set; } // nvarchar(max), null
        public bool? IsSpecialOrder { get; set; } // bit, null
        public int? ProjectID { get; set; } // int, null
        /// <summary>
        /// Có hóa đơn
        /// </summary>
        public bool? IsBill { get; set; } // bit, null
        /// <summary>
        /// Điểm đi
        /// </summary>
        public string StartLocation { get; set; } // nvarchar(max), null
        /// <summary>
        /// Điểm đến
        /// </summary>
        public string EndLocation { get; set; } // nvarchar(max), null
    }
    public enum PaymentOrderModel_Enum{
        ID,
        Code,
        TypeOrder,
        PaymentOrderTypeID,
        DateOrder,
        EmployeeID,
        ReasonOrder,
        DatePayment,
        ReceiverInfo,
        TypePayment,
        AccountNumber,
        Bank,
        TotalMoney,
        TotalMoneyText,
        Unit,
        IsDelete,
        Note,
        TypeBankTransfer,
        ContentBankTransfer,
        AccountingNote,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        DeadlinePayment,
        IsUrgent,
        PONCCID,
        SupplierSaleID,
        CustomerID,
        TypeDocument,
        NumberDocument,
        IsSpecialOrder,
        ProjectID,
        IsBill,
        StartLocation,
        EndLocation,
        }
}
