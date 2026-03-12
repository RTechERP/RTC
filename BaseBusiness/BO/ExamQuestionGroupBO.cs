
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamQuestionGroupBO : BaseBO
	{
		private ExamQuestionGroupFacade facade = ExamQuestionGroupFacade.Instance;
		protected static ExamQuestionGroupBO instance = new ExamQuestionGroupBO();

		protected ExamQuestionGroupBO()
		{
			this.baseFacade = facade;
		}

		public static ExamQuestionGroupBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	