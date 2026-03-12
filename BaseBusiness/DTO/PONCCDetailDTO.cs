using BMS;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.DTO
{
    public class PONCCDetailDTO:PONCCDetailModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductNewCode { get; set; }
        public string Unit { get; set; }
        public int ProductGroupID { get; set; }
    }
}
