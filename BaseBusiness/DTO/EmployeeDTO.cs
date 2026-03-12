using BMS;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.DTO
{
    public class EmployeeDTO : EmployeeModel
    {
        public string DepartmentName { get; set; }
        public int? DepartmentSTT { get; set; }
    }
}
