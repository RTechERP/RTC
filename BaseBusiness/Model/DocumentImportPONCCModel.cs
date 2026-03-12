using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class DocumentImportPONCCModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? PONCCID { get; set; } // int, null
        public int? DocumentImportID { get; set; } // int, null
        /// <summary>
        /// 1:Nhận; 2:Huỷ nhận;3:Khum có
        /// </summary>
        public int? Status { get; set; } // int, null
        public string ReasonCancel { get; set; } // nvarchar(max), null
        public string Note { get; set; } // nvarchar(max), null
        public DateTime? DateRecive { get; set; } // datetime, null
        public int? EmployeeReciveID { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public bool? IsAdditional { get; set; } // bit, null
        public int? EmployeeAdditionalID { get; set; } // int, null
        public DateTime? DateAdditional { get; set; } // datetime, null
        public int? BillImportID { get; set; } // int, null
        /// <summary>
        /// 1:Nhận; 2:Huỷ nhận;3:Khum cần
        /// </summary>
        public int? StatusHR { get; set; } // int, null
        /// <summary>
        /// 1 Đã bàn  giao, 2 Hủy bàn  giao, 3 Không cần 
        /// </summary>
        public int? StatusPurchase { get; set; } // int, null
    }
    public enum DocumentImportPONCCModel_Enum{
        ID,
        PONCCID,
        DocumentImportID,
        Status,
        ReasonCancel,
        Note,
        DateRecive,
        EmployeeReciveID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        IsAdditional,
        EmployeeAdditionalID,
        DateAdditional,
        BillImportID,
        StatusHR,
        StatusPurchase,
        }
}
