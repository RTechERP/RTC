using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPISessionModel : BaseModel
    {
        public int ID { get; set; } // int
        public string Code { get; set; } // nvarchar(50)
        public string Name { get; set; } // nvarchar(250)
        public int? YearEvaluation { get; set; } // int
        public int? QuarterEvaluation { get; set; } // int
        public bool? IsDeleted { get; set; } // bit
        public string CreatedBy { get; set; } // nvarchar(150)
        public DateTime? CreatedDate { get; set; } // datetime
        public string UpdatedBy { get; set; } // nvarchar(150)
        public DateTime? UpdatedDate { get; set; } // datetime
        public int DepartmentID { get; set; } // datetime
    }
    public enum KPISessionModel_Enum
    {
        ID,
        Code,
        Name,
        YearEvaluation,
        QuarterEvaluation,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        DepartmentID,
    }
}
