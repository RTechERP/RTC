
using System;
namespace BMS.Model
{
	public partial class AdminMarketingModel : BaseModel
	{
		public int ID {get; set;}
		
		public string KPI {get; set;}
		
		public decimal CompletionRate {get; set;}
		
		public int Quantity {get; set;}
		
		public int Item {get; set;}
		
	}
}
	