using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ExamResultModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? YearValue { get; set; } // int, null
        /// <summary>
        /// Quý
        /// </summary>
        public int? Season { get; set; } // int, null
        /// <summary>
        /// 1=Vision, 2=Điện, 3=PM, 4=Nội Quy
        /// </summary>
        public int? TestType { get; set; } // int, null
        public int? EmployeeID { get; set; } // int, null
        public int? TotalQuestion { get; set; } // int, null
        public int? TotalChoosen { get; set; } // int, null
        public int? TotalCorrect { get; set; } // int, null
        public int? TotalInCorrect { get; set; } // int, null
        public decimal? TotalMarks { get; set; } // decimal(18,2), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
    }
    public enum ExamResultModel_Enum{
        ID,
        YearValue,
        Season,
        TestType,
        EmployeeID,
        TotalQuestion,
        TotalChoosen,
        TotalCorrect,
        TotalInCorrect,
        TotalMarks,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        }
}
