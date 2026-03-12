
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamQuestionFacade : BaseFacade
	{
		protected static ExamQuestionFacade instance = new ExamQuestionFacade(new ExamQuestionModel());
		protected ExamQuestionFacade(ExamQuestionModel model) : base(model)
		{
		}
		public static ExamQuestionFacade Instance
		{
			get { return instance; }
		}
		protected ExamQuestionFacade():base() 
		{ 
		} 
	
	}
}
	