using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS
{
    public class AccountingContractFileDTO: AccountingContractFileModel
    {
        public int ContractID { get; set; }
        public string ContractNumber { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string ContractGroupText { get; set; }
        public string CustomerOrSupplier { get; set; }
        public bool IsApproved { get; set; }
    }
}
