
using System;
namespace BMS.Model
{
	public partial class KPIDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public string KPI {get; set;}
		
		public string Note {get; set;}
		
		public int GroupSalesID {get; set;}
		
	}
}
	