
using System;
namespace BMS.Model
{
	public partial class StockModel : BaseModel
	{
		public int ID {get; set;}
		
		public string StockCode {get; set;}
		
		public string StockName {get; set;}
		
		public string PhoneNumber {get; set;}
		
		public int StockManager {get; set;}
		
	}
}
	