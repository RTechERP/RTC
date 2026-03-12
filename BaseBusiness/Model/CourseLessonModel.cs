using BMS.Model;
using System;
using System.Collections.Generic;

namespace BMS
{
    public partial class CourseLessonModel : BaseModel
    {
        public int ID { get; set; } // int, not null
        public string Code { get; set; } // varchar(20), null
        public string LessonTitle { get; set; } // nvarchar(400), null
        public string LessonContent { get; set; } // nvarchar(max), null
        public int? Duration { get; set; } // int, null
        public string VideoURL { get; set; } // varchar(300), null
        public string CreatedBy { get; set; } // varchar(50), null
        public DateTime? CreatedDate { get; set; } // datetime, null
        public string UpdatedBy { get; set; } // varchar(50), null
        public DateTime? UpdatedDate { get; set; } // datetime, null
        public int? STT { get; set; } // int, null
        public int? CourseID { get; set; } // int, null
        public int? FileCourseID { get; set; } // int, null
        public string UrlPDF { get; set; } // nvarchar(max), null
        public int? LessonCopyID { get; set; } // int, null
        public bool? IsDeleted { get; set; } // bit, null
        public int EmployeeID { get; set; } // bit, null
    }
    public enum CourseLessonModel_Enum{
        ID,
        Code,
        LessonTitle,
        LessonContent,
        Duration,
        VideoURL,
        CreatedBy,
        CreatedDate,
        UpdatedBy,
        UpdatedDate,
        STT,
        CourseID,
        FileCourseID,
        UrlPDF,
        LessonCopyID,
        IsDeleted,
        EmployeeID
    }
}
