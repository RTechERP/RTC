
using System;
namespace BMS.Model
{
    public partial class ProjectItemDetailModel : BaseModel
    {
        public int ID { get; set; }

        public int STT { get; set; }

        public int ProjectItemID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Type { get; set; }

        public int ParentID { get; set; }

        public bool HasChild { get; set; }

        public int EmployeeID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

    }
}
