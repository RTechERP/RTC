using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIEvaluationFactorsModel : BaseModel
    {
        public int ID { get; set; } // int
        /// <summary>
        /// Năm đánh giá
        /// </summary>
        public int? KPIExamID { get; set; } // int
        /// <summary>
        /// 1: ĐÁNH GIÁ KỸ NĂNG,
        /// 2: CHUYÊN MÔN
        /// </summary>
        public int? EvaluationType { get; set; } // int
        /// <summary>
        /// 1: Kỹ năng; 2: PLC, Robot; 3: VISION; 4: SOFTWARE
        /// </summary>
        public int? SpecializationType { get; set; } // int
        public string STT { get; set; } // nvarchar(250)
        public string EvaluationContent { get; set; } // nvarchar(max)
        /// <summary>
        /// Phương tiện xác minh tiêu chí
        /// </summary>
        public string VerificationToolsContent { get; set; } // nvarchar(max)
        public decimal? StandardPoint { get; set; } // decimal(18,2)
        public int? Coefficient { get; set; } // int
        public int? ParentID { get; set; } // int
        public string Unit { get; set; } // nvarchar(250)
        public bool? IsDeleted { get; set; } // bit
        public string CreatedBy { get; set; } // nvarchar(150)
        public DateTime? CreatedDate { get; set; } // datetime
        public string UpdatedBy { get; set; } // nvarchar(150)
        public DateTime? UpdatedDate { get; set; } // datetime
    }
    public enum KPIEvaluationFactorsModel_Enum{
        ID,
        KPIExamID,
        EvaluationType,
        SpecializationType,
        STT,
        EvaluationContent,
        VerificationToolsContent,
        StandardPoint,
        Coefficient,
        ParentID,
        Unit,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        }
}
