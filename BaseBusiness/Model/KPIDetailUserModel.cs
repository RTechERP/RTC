
using System;
namespace BMS.Model
{
	public partial class KPIDetailUserModel : BaseModel
	{
		public int ID {get; set;}
		
		public int KPIID {get; set;}
		
		public decimal PercentKPI {get; set;}
		
		public int UserID {get; set;}
		
		public int Quy {get; set;}
		
		public int Year {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
	}
}
	