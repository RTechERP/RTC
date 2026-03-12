using BMS;
using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBusiness.DTO
{
    public class PONCCDTO : PONCCModel
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string NameNCC { get; set; }
        public string AddressNCC { get; set; }
        public string CurrencyText { get; set; }
        public string MaSoThue { get; set; }
        public string PhoneNCC { get; set; }
        public string Fax { get; set; }
        public string SoTK { get; set; }
        public string SDTCaNhan { get; set; }
        public string EmailCongTy { get; set; }
        public string CompanyText { get; set; }
        public string SupplierContactName { get; set; }
        public string SupplierContactPhone { get; set; }
        public string SupplierContactEmail { get; set; }
        public string RulePayName { get; set; }
        public string ImageSignName { get; set; }
        public string TaxCompanyCode { get; set; }
    }
}
