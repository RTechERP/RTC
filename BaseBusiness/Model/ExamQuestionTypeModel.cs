
using System;
namespace BMS.Model
{
	public partial class ExamQuestionTypeModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ExamQuestionGroupID {get; set;}
		
		public string TypeCode {get; set;}
		
		public string TypeName {get; set;}

		public decimal ScoreRating { get; set; }
		
	}
}
	