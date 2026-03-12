
using System;
namespace BMS.Model
{
    public partial class AccountingContractLogModel : BaseModel
    {
        public int ID { get; set; }

        public int AccountingContractID { get; set; }

        public string ContentLog { get; set; }

        public DateTime? DateLog { get; set; }

        public bool IsReceivedContract { get; set; }
        public bool IsApproved { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }
}
