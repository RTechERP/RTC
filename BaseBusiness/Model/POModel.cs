
using System;
namespace BMS.Model
{
	public partial class POModel : BaseModel
	{
		public int ID {get; set;}
		
		public string DuanID {get; set;}
		
		public int KhachhangID {get; set;}
		
		public string DuanName {get; set;}
		
		public DateTime? CreateDate {get; set;}
		
		public decimal PriceKhach {get; set;}
		
		public decimal TotalpriceKhach {get; set;}
		
	}
}
	