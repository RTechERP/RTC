
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamQuestionListTestFacade : BaseFacade
	{
		protected static ExamQuestionListTestFacade instance = new ExamQuestionListTestFacade(new ExamQuestionListTestModel());
		protected ExamQuestionListTestFacade(ExamQuestionListTestModel model) : base(model)
		{
		}
		public static ExamQuestionListTestFacade Instance
		{
			get { return instance; }
		}
		protected ExamQuestionListTestFacade():base() 
		{ 
		} 
	
	}
}
	