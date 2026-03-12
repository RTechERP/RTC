
using System;
namespace BMS.Model
{
	public partial class FoodOrderModel : BaseModel
	{
		public int OrderFoodID {get; set;}
		
		public int UserOrder {get; set;}
		
		public int Quantity {get; set;}
		
		public DateTime? DateOrder {get; set;}
		
		public string Note {get; set;}
		
		public bool Confirm {get; set;}
		
	}
}
	