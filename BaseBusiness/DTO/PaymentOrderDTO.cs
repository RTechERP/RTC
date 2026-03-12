using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS
{
    public class PaymentOrderDTO:PaymentOrderModel
    {
        public long RowNum { get; set; }
        public string TypeName { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }
        public string TypeOrderText { get; set; }
        public int Step { get; set; }
        public string StepName { get; set; }
        public int IsApproved { get; set; }
        public string ReasonCancel { get; set; }
        public long TotalPage { get; set; }
    }
}
