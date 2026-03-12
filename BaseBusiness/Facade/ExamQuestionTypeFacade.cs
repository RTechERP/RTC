
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamQuestionTypeFacade : BaseFacade
	{
		protected static ExamQuestionTypeFacade instance = new ExamQuestionTypeFacade(new ExamQuestionTypeModel());
		protected ExamQuestionTypeFacade(ExamQuestionTypeModel model) : base(model)
		{
		}
		public static ExamQuestionTypeFacade Instance
		{
			get { return instance; }
		}
		protected ExamQuestionTypeFacade():base() 
		{ 
		} 
	
	}
}
	