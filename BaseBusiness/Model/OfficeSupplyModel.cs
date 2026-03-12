
using System;
namespace BMS.Model
{
	public partial class OfficeSupplyModel : BaseModel
	{
		public int ID {get; set;}
		
		public string CodeRTC {get; set;}
		
		public string CodeNCC {get; set;}
		
		public string NameRTC {get; set;}
		
		public string NameNCC {get; set;}
		
		public int SupplyUnitID {get; set;}
		
		public decimal Price {get; set;}
		
		public int RequestLimit {get; set;}
		
		public int Type {get; set;}
		
	}
}
	