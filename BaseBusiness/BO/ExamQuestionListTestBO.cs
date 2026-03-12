
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamQuestionListTestBO : BaseBO
	{
		private ExamQuestionListTestFacade facade = ExamQuestionListTestFacade.Instance;
		protected static ExamQuestionListTestBO instance = new ExamQuestionListTestBO();

		protected ExamQuestionListTestBO()
		{
			this.baseFacade = facade;
		}

		public static ExamQuestionListTestBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	