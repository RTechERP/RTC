using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BillImportQCModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        /// <summary>
        /// Nhân viên yêu cầu
        /// </summary>
        public int? EmployeeRequestID { get; set; } // int, null
        /// <summary>
        /// Mã billimport
        /// </summary>
        public string RequestImportCode { get; set; } // nvarchar(150), null
        /// <summary>
        /// Ngày yêu cầu QC
        /// </summary>
        public DateTime? RequestDateQC { get; set; } // datetime, null
        public bool? IsDeleted { get; set; } // bit, null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? Dealine { get; set; } // datetime, null
    }
    public enum BillImportQCModel_Enum{
        ID,
        EmployeeRequestID,
        RequestImportCode,
        RequestDateQC,
        IsDeleted,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        Dealine,
        }
}
