
using System;
namespace BMS.Model
{
	public partial class PODetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int POID {get; set;}
		
		public int BillID {get; set;}
		
		public string TenSP {get; set;}
		
		public decimal SoLuong {get; set;}
		
		public string ModelDuan {get; set;}
		
		public string ModelChuan {get; set;}
		
	}
}
	