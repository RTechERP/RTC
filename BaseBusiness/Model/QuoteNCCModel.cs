
using System;
namespace BMS.Model
{
	public partial class QuoteNCCModel : BaseModel
	{
		public int ID {get; set;}
		
		public string GroupID {get; set;}
		
		public int SupplierID {get; set;}
		
		public bool IsApproved {get; set;}
		
		public string QuoteCode {get; set;}
		
		public string UserNCC {get; set;}
		
		public string UserName {get; set;}
		
		public string Phone {get; set;}
		
		public int TotalMoney {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public DateTime? QuoteDate {get; set;}
		
	}
}
	