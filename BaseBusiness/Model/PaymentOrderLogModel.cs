using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class PaymentOrderLogModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? PaymentOrderID { get; set; } // int, null
        public int? Step { get; set; } // int, null
        public string StepName { get; set; } // nvarchar(max), null
        public DateTime? DateApproved { get; set; } // datetime, null
        /// <summary>
        /// 0:Chờ duyêt; 1:Đã duyệt; 2:Không duyệt
        /// </summary>
        public int? IsApproved { get; set; } // int, null
        /// <summary>
        /// Người được chỉ định duyệt
        /// </summary>
        public int? EmployeeID { get; set; } // int, null
        /// <summary>
        /// Người duyệt thực tế
        /// </summary>
        public int? EmployeeApproveActualID { get; set; } // int, null
        public string ReasonCancel { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string ContentLog { get; set; } // nvarchar(max), null
        /// <summary>
        /// Kế toán yc bổ sung (1: Yc bổ sung file)
        /// </summary>
        public bool? IsRequestAppendFileAC { get; set; } // bit, null
        /// <summary>
        /// HR yc bổ sung (1: Yc bổ sung file)
        /// </summary>
        public bool? IsRequestAppendFileHR { get; set; } // bit, null
        /// <summary>
        /// Lý do Kế toán yc bổ sung file
        /// </summary>
        public string ReasonRequestAppendFileAC { get; set; } // nvarchar(550), null
        /// <summary>
        /// Lý do HR yc bổ sung file
        /// </summary>
        public string ReasonRequestAppendFileHR { get; set; } // nvarchar(550), null
    }
    public enum PaymentOrderLogModel_Enum{
        ID,
        PaymentOrderID,
        Step,
        StepName,
        DateApproved,
        IsApproved,
        EmployeeID,
        EmployeeApproveActualID,
        ReasonCancel,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        ContentLog,
        IsRequestAppendFileAC,
        IsRequestAppendFileHR,
        ReasonRequestAppendFileAC,
        ReasonRequestAppendFileHR,
        }
}
