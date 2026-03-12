
using System;
namespace BMS.Model
{
	public partial class CourseQuestionModel : BaseModel
	{
		public int ID {get; set;}
		
		public string QuestionText {get; set;}
		
		public int STT {get; set;}
		
		public int CourseExamId {get; set;}
		
		public int CheckInput {get; set;}
		
		public int Marks {get; set;}
		public string Image {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	