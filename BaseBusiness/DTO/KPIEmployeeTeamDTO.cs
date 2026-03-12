using BMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.DTO
{
    public class KPIEmployeeTeamDTO: KPIEmployeeTeamModel
    {
        public string LeaderName { get; set; }
        public string DepartmentName { get; set; }
    }
}
