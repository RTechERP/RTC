
using System;
namespace BMS.Model
{
	public partial class ExamCategoryModel : BaseModel
	{
		public int ID {get; set;}
		
		public string CatCode {get; set;}
		
		public string CatName {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public bool Status {get; set;}
		
		public int Year {get; set;}
		
		public int Quy {get; set;}
		
	}
}
	