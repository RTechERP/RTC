
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamResultAnswerDetailFacade : BaseFacade
	{
		protected static ExamResultAnswerDetailFacade instance = new ExamResultAnswerDetailFacade(new ExamResultAnswerDetailModel());
		protected ExamResultAnswerDetailFacade(ExamResultAnswerDetailModel model) : base(model)
		{
		}
		public static ExamResultAnswerDetailFacade Instance
		{
			get { return instance; }
		}
		protected ExamResultAnswerDetailFacade():base() 
		{ 
		} 
	
	}
}
	