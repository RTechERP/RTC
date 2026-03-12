using BMS;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.DTO
{
    public class ProjectPartlistPriceRequestDTO: ProjectPartlistPriceRequestModel
    {
        public string StatusRequestText { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }
        public string TT { get; set; }
        public int ParentID { get; set; }
    }
}
