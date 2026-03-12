using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class KPISumarizeModel : BaseModel
    {
        public int ID { get; set; } // int
        public int? YearEvalution { get; set; } // int
        public int? QuarterEvalution { get; set; } // int
        public int? EmployeeID { get; set; } // int
        /// <summary>
        /// Thời gian, giờ giấc
        /// </summary>
        public int? TimeHours { get; set; } // int
        /// <summary>
        /// 5S quy trình, quy định
        /// </summary>
        public int? FiveSRegulatedProcedures { get; set; } // int
        /// <summary>
        /// Chuẩn bị hàng và báo cáo công việc
        /// </summary>
        public int? PrepareGoodsReport { get; set; } // int
        /// <summary>
        /// Tinh thần làm việc
        /// </summary>
        public int? AttitudeTowardsCustomers { get; set; } // int
        /// <summary>
        /// Làm mất thiết bị
        /// </summary>
        public int? LossEquipment { get; set; } // int
        /// <summary>
        /// Điểm kỹ năng
        /// </summary>
        public decimal? SkillPoints { get; set; } // decimal(18,2)
        /// <summary>
        /// Điểm chuyên môn PLC
        /// </summary>
        public decimal? PLCExpertisePoints { get; set; } // decimal(18,2)
        /// <summary>
        /// Điểm chuyên môn Vision
        /// </summary>
        public decimal? VisionExpertisePoints { get; set; } // decimal(18,2)
        /// <summary>
        /// Điểm chuyên môn Software
        /// </summary>
        public decimal? SoftwareExpertisePoints { get; set; } // decimal(18,2)
    }
    public enum KPISumarizeModel_Enum{
        ID,
        YearEvalution,
        QuarterEvalution,
        EmployeeID,
        TimeHours,
        FiveSRegulatedProcedures,
        PrepareGoodsReport,
        AttitudeTowardsCustomers,
        LossEquipment,
        SkillPoints,
        PLCExpertisePoints,
        VisionExpertisePoints,
        SoftwareExpertisePoints,
        }
}
