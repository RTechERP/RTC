using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class PaymentOrderTypeDocumentModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? PaymentOrderID { get; set; } // int, null
        /// <summary>
        /// 1: PO, 2: Hóa đơn
        /// </summary>
        public int? TypeDocumentID { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum PaymentOrderTypeDocumentModel_Enum{
        ID,
        PaymentOrderID,
        TypeDocumentID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
