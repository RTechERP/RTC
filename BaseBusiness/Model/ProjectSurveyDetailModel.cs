using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class ProjectSurveyDetailModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? ProjectSurveyID { get; set; } // int, null
        public int? ProjectTypeID { get; set; } // int, null
        /// <summary>
        /// Nhân viên kỹ thuật khảo sát
        /// </summary>
        public int? EmployeeID { get; set; } // int, null
        public DateTime? DateSurvey { get; set; } // datetime, null
        /// <summary>
        /// 1: Duyệt; 2: Hủy duyệt
        /// </summary>
        public int? Status { get; set; } // int, null
        public string ReasonCancel { get; set; } // nvarchar(550), null
        public string Note { get; set; } // nvarchar(550), null
        public string CreatedBy { get; set; } // nvarchar(100), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // nvarchar(100), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? LeaderID { get; set; } // int, null
        public string Result { get; set; } // nvarchar(max), null
        /// <summary>
        /// Buổi khảo sát (1: Buổi sáng; 2: Buổi chiều)
        /// </summary>
        public int? SurveySession { get; set; } // int, null
    }
    public enum ProjectSurveyDetailModel_Enum{
        ID,
        ProjectSurveyID,
        ProjectTypeID,
        EmployeeID,
        DateSurvey,
        Status,
        ReasonCancel,
        Note,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        LeaderID,
        Result,
        SurveySession,
        }
}
