
using System;
namespace BMS.Model
{
	public partial class EmployeePurchaseModel : BaseModel
	{
		public int ID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public int TaxCompayID {get; set;}
		
		public string Telephone {get; set;}
		
		public string Email {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		public string FullName {get; set;}
		
	}
}
	