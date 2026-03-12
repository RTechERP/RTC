
using System;
namespace BMS.Model
{
    public partial class CourseFileModel : BaseModel
    {
        public int ID { get; set; }

        public string NameFile { get; set; }

        public int CourseID { get; set; }

        public int LessonID { get; set; }
        public string OriginPath { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
