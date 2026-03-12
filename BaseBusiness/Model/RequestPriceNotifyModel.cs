
using System;
namespace BMS.Model
{
	public partial class RequestPriceNotifyModel : BaseModel
	{
		public long ID {get; set;}
		
		public string RequestPriceCode {get; set;}
		
		public int RequestPriceID {get; set;}
		
		public int UserID {get; set;}
		
		public bool IsShow {get; set;}
		
	}
}
	