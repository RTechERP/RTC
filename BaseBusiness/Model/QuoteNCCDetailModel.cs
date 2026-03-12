
using System;
namespace BMS.Model
{
	public partial class QuoteNCCDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int QuoteNCCID {get; set;}
		
		public int ProductID {get; set;}
		
		public int Qty {get; set;}
		
		public int UnitPrice {get; set;}
		
		public int IntoMoney {get; set;}
		
		public string IntendTime {get; set;}
		
	}
}
	