
using System;
namespace BMS.Model
{
	public partial class AddressStockModel : BaseModel
	{
		public int ID {get; set;}
		
		public string Address {get; set;}
		
		public int CustomerID {get; set;}
		
	}
}
	