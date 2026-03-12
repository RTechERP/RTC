
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamResultAnswerDetailBO : BaseBO
	{
		private ExamResultAnswerDetailFacade facade = ExamResultAnswerDetailFacade.Instance;
		protected static ExamResultAnswerDetailBO instance = new ExamResultAnswerDetailBO();

		protected ExamResultAnswerDetailBO()
		{
			this.baseFacade = facade;
		}

		public static ExamResultAnswerDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	