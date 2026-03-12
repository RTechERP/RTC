
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamQuestionGroupFacade : BaseFacade
	{
		protected static ExamQuestionGroupFacade instance = new ExamQuestionGroupFacade(new ExamQuestionGroupModel());
		protected ExamQuestionGroupFacade(ExamQuestionGroupModel model) : base(model)
		{
		}
		public static ExamQuestionGroupFacade Instance
		{
			get { return instance; }
		}
		protected ExamQuestionGroupFacade():base() 
		{ 
		} 
	
	}
}
	