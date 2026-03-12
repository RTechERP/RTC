using BMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.DTO
{
    public class KPIEmployeeTeamLinkDTO: KPIEmployeeTeamLinkModel
    {
        public string Team { get; set; }
        public string FullName { get; set; }
        public string Code { get; set; }
    }
}
