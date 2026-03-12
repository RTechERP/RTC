using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPIEvaluationPointModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? KPIEvaluationFactorsID { get; set; } // int, null
        public int? EmployeeID { get; set; } // int, null
        public decimal? EmployeePoint { get; set; } // decimal(18,2), null
        public decimal? TBPPoint { get; set; } // decimal(18,2), null
        public int? TBPID { get; set; } // int, null
        public decimal? BGDPoint { get; set; } // decimal(18,2), null
        public int? BGDID { get; set; } // int, null
        public decimal? EmployeeEvaluation { get; set; } // decimal(18,2), null
        public decimal? EmployeeCoefficient { get; set; } // decimal(18,2), null
        public decimal? TBPEvaluation { get; set; } // decimal(18,2), null
        public decimal? TBPCoefficient { get; set; } // decimal(18,2), null
        public decimal? BGDEvaluation { get; set; } // decimal(18,2), null
        public decimal? BGDCoefficient { get; set; } // decimal(18,2), null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        /// <summary>
        /// 1: Hoàn thành; 0: Chưa hoàn thành
        /// </summary>
        public int? Status { get; set; } // int, null
        public bool? IsAdminConfirm { get; set; } // bit, null
        public decimal? TBPPointInput { get; set; } // decimal(18,2), null
        public decimal? BGDPointInput { get; set; } // decimal(18,2), null
        public DateTime? DateEmployeeConfirm { get; set; } // datetime, null
        public DateTime? DateTBPConfirm { get; set; } // datetime, null
        public DateTime? DateBGDConfirm { get; set; } // datetime, null
    }
    public enum KPIEvaluationPointModel_Enum{
        ID,
        KPIEvaluationFactorsID,
        EmployeeID,
        EmployeePoint,
        TBPPoint,
        TBPID,
        BGDPoint,
        BGDID,
        EmployeeEvaluation,
        EmployeeCoefficient,
        TBPEvaluation,
        TBPCoefficient,
        BGDEvaluation,
        BGDCoefficient,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        Status,
        IsAdminConfirm,
        TBPPointInput,
        BGDPointInput,
        DateEmployeeConfirm,
        DateTBPConfirm,
        DateBGDConfirm,
        }
}
