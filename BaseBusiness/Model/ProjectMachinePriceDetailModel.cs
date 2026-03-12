using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectMachinePriceDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProjectMachinePriceID { get; set; } // int, null
        public int? ProjectVersionID { get; set; } // int, null
        public int? STT { get; set; } // int, null
        public string CodeGroup { get; set; } // varchar(550), null
        public string NameGroup { get; set; } // nvarchar(550), null
        /// <summary>
        /// Nội dung
        /// </summary>
        public string ContentPrice { get; set; } // nvarchar(550), null
        /// <summary>
        /// Số tiền chi
        /// </summary>
        public decimal? AmountSpent { get; set; } // decimal(18,2), null
        /// <summary>
        /// Đối tượng phụ trách
        /// </summary>
        public string DependentObject { get; set; } // nvarchar(550), null
        /// <summary>
        /// Chi phí dự toán
        /// </summary>
        public decimal? EstimateCost { get; set; } // decimal(18,2), null
        /// <summary>
        /// Hệ số
        /// </summary>
        public decimal? Coefficient { get; set; } // decimal(18,2), null
        public string Note { get; set; } // nvarchar(max), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public string CreatedBy { get; set; } // nvarchar(150), null
        public string UpdatedBy { get; set; } // nvarchar(150), null
    }
    public enum ProjectMachinePriceDetailModel_Enum{
        ID,
        ProjectMachinePriceID,
        ProjectVersionID,
        STT,
        CodeGroup,
        NameGroup,
        ContentPrice,
        AmountSpent,
        DependentObject,
        EstimateCost,
        Coefficient,
        Note,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        }
}
