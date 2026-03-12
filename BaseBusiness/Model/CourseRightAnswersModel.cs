
using System;
namespace BMS.Model
{
	public partial class CourseRightAnswersModel : BaseModel
	{
		public int ID {get; set;}
		
		public int CourseQuestionID {get; set;}
		
		public int CourseAnswerID {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	