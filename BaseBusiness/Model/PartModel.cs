
using System;
namespace BMS.Model
{
	public partial class PartModel : BaseModel
	{
		public int ID {get; set;}
		
		public int PartGroupID {get; set;}
		
		public int ManufacturerID {get; set;}
		
		public string PartCode {get; set;}
		
		public string PartCodeRTC {get; set;}
		
		public string PartName {get; set;}
		
		public string PartNameRTC {get; set;}
		
		public int Status {get; set;}
		
		public decimal Price {get; set;}
		
		public string Description {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public bool IsDeleted {get; set;}
		
	}
}
	