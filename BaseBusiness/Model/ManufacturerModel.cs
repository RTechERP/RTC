
using System;
namespace BMS.Model
{
	public partial class ManufacturerModel : BaseModel
	{
		public int ID {get; set;}
		
		public string ManufacturerCode {get; set;}
		
		public string ManufacturerName {get; set;}
		
		public string Note {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	