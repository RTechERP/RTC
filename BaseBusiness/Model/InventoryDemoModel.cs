
using System;
namespace BMS.Model
{
	public partial class InventoryDemoModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProductRTCID {get; set;}
		
		public int WarehouseID {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	