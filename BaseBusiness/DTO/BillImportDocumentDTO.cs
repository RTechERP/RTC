using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.DTO
{
    public class BillImportDocumentDTO:BillImportDocumentModel
    {
        public string DocumentTypeText { get; set; }
        public string StatusDocumentText { get; set; }
    }
}
