using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class BookingRoomLogModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public DateTime? DateLog { get; set; } // datetime, null
        public string ContentLog { get; set; } // nvarchar(max), null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
    }
    public enum BookingRoomLogModel_Enum
    {
        ID,
        DateLog,
        ContentLog,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
    }
}
