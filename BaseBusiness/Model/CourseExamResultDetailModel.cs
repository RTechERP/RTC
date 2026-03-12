
using System;
namespace BMS.Model
{
	public partial class CourseExamResultDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int CourseQuestionId {get; set;}
		
		public int CourseAnswerId {get; set;}
		
		public int CourseExamResultId {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	