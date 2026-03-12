
using System;
namespace BMS.Model
{
	public partial class DiscountOrderModel : BaseModel
	{
		public int ID {get; set;}
		
		public int Total {get; set;}
		
		public int MoneyHistory {get; set;}
		
		public int Discount {get; set;}
		
		public int Month {get; set;}
		
		public int Year {get; set;}
		
		public int UserID {get; set;}
		
		public int Week {get; set;}
		
		public decimal Ratio {get; set;}
		
	}
}
	