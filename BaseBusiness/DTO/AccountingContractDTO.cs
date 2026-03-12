using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS
{
    public class AccountingContractDTO: AccountingContractModel
    {
        public string CompanyName { get; set; }
        public string ContractGroupText { get; set; }
        public string TypeName { get; set; }
        public string CustomerOrSupplier { get; set; }
        public string FullName { get; set; }
        public int IsComingExpired { get; set; }
        public int TotalPage { get; set; }
        public long RowNumber { get; set; }
        public string FileNames { get; set; }
        public string CreatedName { get; set; }
    }
}
