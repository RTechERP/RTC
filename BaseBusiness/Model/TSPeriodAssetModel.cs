
using System;
namespace BMS.Model
{
	public partial class TSPeriodAssetModel : BaseModel
	{
		public int ID {get; set;}
		
		public int AssetManagementID {get; set;}
		
		public int PeriodMaintenance {get; set;}
		
		public DateTime? DateMaintenanceNearest {get; set;}
		
		public DateTime? DateMaintenanceNext {get; set;}
		
		public string Note {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	