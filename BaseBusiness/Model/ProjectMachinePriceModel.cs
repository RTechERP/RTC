using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectMachinePriceModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProjectID { get; set; } // int, null
        public int? EmployeeID { get; set; } // int, null
        /// <summary>
        /// Ngày thanh toán
        /// </summary>
        public DateTime? DatePrice { get; set; } // datetime, null
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; } // datetime, null
        /// <summary>
        /// Ngày update
        /// </summary>
        public DateTime? UpdatedDate { get; set; } // datetime, null
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; } // nvarchar(150), null
        /// <summary>
        /// Người update
        /// </summary>
        public string UpdatedBy { get; set; } // nvarchar(150), null
        public bool? IsDelete { get; set; } // bit, null
    }
    public enum ProjectMachinePriceModel_Enum{
        ID,
        ProjectID,
        EmployeeID,
        DatePrice,
        CreatedDate,
        UpdatedDate,
        CreatedBy,
        UpdatedBy,
        IsDelete,
        }
}
