using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class JobRequirementLogModel : BaseModel
    {
        public int ID { get; set; } // int
        public int? JobRequirementID { get; set; } // int
        public int? EmployeeID { get; set; } // int
        public DateTime? DateLog { get; set; } // datetime
        public string LogContent { get; set; } // nvarchar(max)
        public string CreatedBy { get; set; } // nvarchar(50)
        public DateTime? CreatedDate { get; set; } // datetime
        public string UpdatedBy { get; set; } // nvarchar(50)
        public DateTime? UpdatedDate { get; set; } // datetime
    }
    public enum JobRequirementLogModel_Enum{
        ID,
        JobRequirementID,
        EmployeeID,
        DateLog,
        LogContent,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
