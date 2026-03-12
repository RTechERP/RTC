
using System;
namespace BMS.Model
{
	public partial class ExamTestResultModel : BaseModel
	{
		public int ID {get; set;}
		
		public int ExamCategoryID {get; set;}
		
		public int ExamListTestID {get; set;}
		
		public int ExamQuestionBankID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public string CandidateName {get; set;}
		
		public string ResultChose {get; set;}
		
	}
}
	