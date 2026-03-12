
using System;
namespace BMS.Model
{
	public partial class RequestPaidPOModel : BaseModel
	{
		public int ID {get; set;}
		
		public int PurchaseOrderID {get; set;}
		
		public decimal TotalPrice {get; set;}
		
		public DateTime? DatePaidExpected {get; set;}
		
		public DateTime? DatePaid {get; set;}
		
		public int RequestPaidPOStatus {get; set;}
		
	}
}
	