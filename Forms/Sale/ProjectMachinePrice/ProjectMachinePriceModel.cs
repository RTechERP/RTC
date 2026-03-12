using System;

namespace BMS.Model
{
    public partial class ProjectMachinePriceModel: BaseModel
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime? DatePrice { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDelete { get; set; }
    }
}
