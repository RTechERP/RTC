
using System;
namespace BMS.Model
{
	public partial class LocationModel : BaseModel
	{
		public int ID {get; set;}
		
		public string LocationCode {get; set;}
		
		public string LocationName {get; set;}
		public int ProductGroupID { get; set;}
		
	}
}
	