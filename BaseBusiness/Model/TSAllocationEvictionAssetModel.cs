
using System;
namespace BMS.Model
{
	public partial class TSAllocationEvictionAssetModel : BaseModel
	{
		public int ID {get; set;}
		
		public int AssetManagementID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public int ChucVuID {get; set;}
		
		public int DepartmentID {get; set;}
		
		public DateTime? DateAllocation {get; set;}
		
		public string Status {get; set;}
		
		public string Note {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	