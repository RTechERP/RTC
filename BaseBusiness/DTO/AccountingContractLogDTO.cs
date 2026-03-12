using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS
{
    public class AccountingContractLogDTO: AccountingContractLogModel
    {
        public string ContractNumber { get; set; }
        public string IsReceivedContractText { get; set; }
        public string FullName { get; set; }
        public string IsApprovedText { get; set; }
    }
}
