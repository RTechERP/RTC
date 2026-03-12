
using System;
namespace BMS.Model
{
	public partial class SupplierModel : BaseModel
	{
		public int ID {get; set;}
		
		public string SupplierName {get; set;}
		
		public string SupplierCode {get; set;}
		
		public string SupplierShortName {get; set;}
		
		public string Phone {get; set;}
		
		public string Email {get; set;}
		
		public string Website {get; set;}
		
		public string ContactName {get; set;}
		
		public string ContactPhone {get; set;}
		
		public string ContactEmail {get; set;}
		
		public string Note {get; set;}
		
		public string MST {get; set;}
		
		public string BankName {get; set;}
		
		public string BankAcount {get; set;}
		
		public string BankAcountName {get; set;}
		
		public string Office {get; set;}
		
		public string Address {get; set;}
		
		public string MainProduct {get; set;}
		
		public decimal DebtLimit {get; set;}
		
		public bool IsDeleted {get; set;}
		
		public string SkypeID {get; set;}
		
		public string Manufactures {get; set;}
		
		public string Advantages {get; set;}
		
		public string Defect {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	