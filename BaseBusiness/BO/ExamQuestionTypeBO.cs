
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamQuestionTypeBO : BaseBO
	{
		private ExamQuestionTypeFacade facade = ExamQuestionTypeFacade.Instance;
		protected static ExamQuestionTypeBO instance = new ExamQuestionTypeBO();

		protected ExamQuestionTypeBO()
		{
			this.baseFacade = facade;
		}

		public static ExamQuestionTypeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	