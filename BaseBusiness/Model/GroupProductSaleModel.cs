
using System;
namespace BMS.Model
{
	public partial class GroupProductSaleModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ProductGroupID {get; set;}
		
		public string GroupName {get; set;}
		
	}
}
	