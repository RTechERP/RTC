
using System;
namespace BMS.Model
{
	public partial class AccountingBillModel : BaseModel
	{
		public int ID {get; set;}
		
		public string BillNumber {get; set;}
		
		public DateTime? BillDate {get; set;}
		
		public string SupplierSale {get; set;}
		
		public int SupplierSaleID {get; set;}
		
		public decimal TotalMoney {get; set;}
		
		public int CurrencyID {get; set;}
		
		public int TaxCompanyID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public int EmployeeStatus {get; set;}
		
		public int DeliverTaxStatus {get; set;}
		
		public DateTime? DeliverTaxDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public bool IsDeleted {get; set;}
		
	}
}
	