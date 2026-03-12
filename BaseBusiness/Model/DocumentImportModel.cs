
using System;
namespace BMS.Model
{
	public partial class DocumentImportModel : BaseModel
	{
		public int ID {get; set;}
		
		public string DocumentImportCode {get; set;}
		
		public string DocumentImportName {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public bool IsDeleted {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	