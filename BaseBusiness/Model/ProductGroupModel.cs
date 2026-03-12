
using System;
namespace BMS.Model
{
	public partial class ProductGroupModel : BaseModel
	{
		public int ID {get; set;}
		
		public string ProductGroupID {get; set;}
		
		public string ProductGroupName {get; set;}
		
		public bool IsVisible {get; set;}
		public int EmployeeID { get; set;}
		
	}
}
	