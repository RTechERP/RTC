using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class PaymentOrderFileBankSlipModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? PaymentOrderID { get; set; } // int, null
        public string FileName { get; set; } // nvarchar(550), null
        public string OriginPath { get; set; } // nvarchar(max), null
        public string ServerPath { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum PaymentOrderFileBankSlipModel_Enum{
        ID,
        PaymentOrderID,
        FileName,
        OriginPath,
        ServerPath,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
