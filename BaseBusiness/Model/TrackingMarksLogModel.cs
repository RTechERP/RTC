using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class TrackingMarksLogModel : BaseModel
    {
        public int ID { get; set; } // int
        public int? TrackingMarksID { get; set; } // int
        public DateTime? DateLog { get; set; } // datetime
        public int? EmployeeID { get; set; } // int
        public string ContentLog { get; set; } // nvarchar(max)
        public DateTime? CreatedDate { get; set; } // datetime
        public string CreatedBy { get; set; } // nvarchar(50)
        public DateTime? UpdatedDate { get; set; } // datetime
        public string UpdatedBy { get; set; } // nvarchar(50)
    }
    public enum TrackingMarksLogModel_Enum{
        ID,
        TrackingMarksID,
        DateLog,
        EmployeeID,
        ContentLog,
        CreatedDate,
        CreatedBy,
        UpdatedDate,
        UpdatedBy,
        }
}
