
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamResultBO : BaseBO
	{
		private ExamResultFacade facade = ExamResultFacade.Instance;
		protected static ExamResultBO instance = new ExamResultBO();

		protected ExamResultBO()
		{
			this.baseFacade = facade;
		}

		public static ExamResultBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	