
using System;
namespace BMS.Model
{
	public partial class PaymentOrderCustomerModel : BaseModel
	{
		public int ID {get; set;}
		
		public int PaymentOrderID {get; set;}
		
		public int CustomerID {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	