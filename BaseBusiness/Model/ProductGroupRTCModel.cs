
using System;
namespace BMS.Model
{
	public partial class ProductGroupRTCModel : BaseModel
	{
		public int ID {get; set;}
		
		public string ProductGroupNo {get; set;}
		
		public string ProductGroupName {get; set;}
        public int NumberOrder { get; set; }
		public int WarehouseID { get; set; }
	}
}
	