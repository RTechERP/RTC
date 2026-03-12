using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class PaymentOrderOrderTypeModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? PaymentOrderID { get; set; } // int, null
        public int? PaymentOrderTypeID { get; set; } // int, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum PaymentOrderOrderTypeModel_Enum{
        ID,
        PaymentOrderID,
        PaymentOrderTypeID,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
