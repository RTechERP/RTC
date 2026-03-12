using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class PaymentOrderTypeOrderModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? PaymentOrderID { get; set; } // int, null
        /// <summary>
        /// 1: Đề nghị tạm ứng, 2: Đề nghị thanh toán/quyết toán
        /// </summary>
        public int? TypeOrderID { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum PaymentOrderTypeOrderModel_Enum{
        ID,
        PaymentOrderID,
        TypeOrderID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
