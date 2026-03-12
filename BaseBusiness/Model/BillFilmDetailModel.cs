
using System;
namespace BMS.Model
{
	public partial class BillFilmDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProductFimID {get; set;}
		
		public int BillID {get; set;}
		
		public int Qty {get; set;}
		
		public int NeededBigBoxQty {get; set;}
		
	}
}
	