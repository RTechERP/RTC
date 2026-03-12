
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamQuestionBankFacade : BaseFacade
	{
		protected static ExamQuestionBankFacade instance = new ExamQuestionBankFacade(new ExamQuestionBankModel());
		protected ExamQuestionBankFacade(ExamQuestionBankModel model) : base(model)
		{
		}
		public static ExamQuestionBankFacade Instance
		{
			get { return instance; }
		}
		protected ExamQuestionBankFacade():base() 
		{ 
		} 
	
	}
}
	