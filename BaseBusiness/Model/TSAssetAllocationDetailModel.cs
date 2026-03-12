
using System;
namespace BMS.Model
{
	public partial class TSAssetAllocationDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int TSAssetAllocationID {get; set;}
		
		public int AssetManagementID {get; set;}
		
		public int STT {get; set;}
		
		public int Quantity {get; set;}
		
		public string Note {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
        public int EmployeeID { get; set; }
    }
}
	