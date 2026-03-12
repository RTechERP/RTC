
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamQuestionBO : BaseBO
	{
		private ExamQuestionFacade facade = ExamQuestionFacade.Instance;
		protected static ExamQuestionBO instance = new ExamQuestionBO();

		protected ExamQuestionBO()
		{
			this.baseFacade = facade;
		}

		public static ExamQuestionBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	