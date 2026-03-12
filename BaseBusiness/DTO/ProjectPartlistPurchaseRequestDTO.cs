using BMS;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.DTO
{
    public class ProjectPartlistPurchaseRequestDTO: ProjectPartlistPurchaseRequestModel
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime? Deadline { get; set; }
        public string PONCCDetailRequestBuyID { get; set; }
        public string GuestCode { get; set; }
        public string PONumber { get; set; }
        public string CustomerCode { get; set; }
        public bool IsCommercialProduct { get; set; }
        public bool IsBill { get; set; }
        public bool IsStock { get; set; }
        public string SpecialCode { get; set; }
    }
}
