using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class RequestInvoiceModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? STT { get; set; } // int, null
        public string Code { get; set; } // varchar(50), null
        public DateTime? DateRequest { get; set; } // datetime, null
        public int? CustomerID { get; set; } // int, null
        /// <summary>
        /// Công ty bán
        /// </summary>
        public int? TaxCompanyID { get; set; } // int, null
        /// <summary>
        /// ID của người gửi yêu cầu (Lấy từ bảng employee)
        /// </summary>
        public int? EmployeeRequestID { get; set; } // int, null
        /// <summary>
        /// ID của người nhận yêu cầu (Lấy từ bảng employee)
        /// </summary>
        public int? ReceriverID { get; set; } // int, null
        /// <summary>
        /// 1: YC xuất HĐ, 2: Đã xuất nháp, 3: Đã phát hành hóa đơn
        /// </summary>
        public int? Status { get; set; } // int, null
        public string Note { get; set; } // nvarchar(550), null
        public bool? IsDeleted { get; set; } // bit, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsUrgency { get; set; } // bit, null
        public DateTime? DealineUrgency { get; set; } // datetime, null
        public string AmendReason { get; set; } // datetime, null
        public bool? IsCustomsDeclared { get; set; } // datetime, null
    }
    public enum RequestInvoiceModel_Enum
    {
        ID,
        STT,
        Code,
        DateRequest,
        CustomerID,
        TaxCompanyID,
        EmployeeRequestID,
        ReceriverID,
        Status,
        Note,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsUrgency,
        DealineUrgency,
        AmendReason,
        IsCustomsDeclared
    }
}
