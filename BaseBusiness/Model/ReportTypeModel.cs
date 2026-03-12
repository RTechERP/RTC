
using System;
namespace BMS.Model
{
	public partial class ReportTypeModel : BaseModel
	{
		public int ID {get; set;}
		
		public string ReportTypeName {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	