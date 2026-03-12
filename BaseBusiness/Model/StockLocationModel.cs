
using System;
namespace BMS.Model
{
	public partial class StockLocationModel : BaseModel
	{
		public int ID {get; set;}
		
		public string StockLocationCode {get; set;}
		
		public string StockLocationName {get; set;}
		
		public int StockCode {get; set;}
		
	}
}
	