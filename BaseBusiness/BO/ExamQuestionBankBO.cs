
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamQuestionBankBO : BaseBO
	{
		private ExamQuestionBankFacade facade = ExamQuestionBankFacade.Instance;
		protected static ExamQuestionBankBO instance = new ExamQuestionBankBO();

		protected ExamQuestionBankBO()
		{
			this.baseFacade = facade;
		}

		public static ExamQuestionBankBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	