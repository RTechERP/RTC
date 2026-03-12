using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.DTO
{
    public class InvoiceDTO
    {
        public int IdMapping { get; set; }
        public List<InvoiceLinkModel> Details { get; set; }
    }
}
