using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class CourseModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public int? STT { get; set; } // int, null
        public string Code { get; set; } // varchar(20), null
        public string NameCourse { get; set; } // nvarchar(300), null
        public string Instructor { get; set; } // nvarchar(200), null
        public int? CourseCatalogID { get; set; } // int, null
        public bool? DeleteFlag { get; set; } // bit, null
        public int? FileCourseID { get; set; } // int, null
        public bool? IsPractice { get; set; } // bit, null
        public string CreatedBy { get; set; } // varchar(20), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // varchar(20), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public decimal? QuestionCount { get; set; } // decimal(18,2), null
        public decimal? QuestionDuration { get; set; } // decimal(18,2), null
        public decimal? LeadTime { get; set; } // decimal(18,2), null
        public int? CourseCopyID { get; set; } // int, null
        public int? CourseTypeID { get; set; } // int, null
        public int? EmployeeID { get; set; } // int, null
    }
    public enum CourseModel_Enum{
        ID,
        STT,
        Code,
        NameCourse,
        Instructor,
        CourseCatalogID,
        DeleteFlag,
        FileCourseID,
        IsPractice,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        QuestionCount,
        QuestionDuration,
        LeadTime,
        CourseCopyID,
        CourseTypeID,
        EmployeeID,
        }
}
