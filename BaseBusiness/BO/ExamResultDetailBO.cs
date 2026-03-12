
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamResultDetailBO : BaseBO
	{
		private ExamResultDetailFacade facade = ExamResultDetailFacade.Instance;
		protected static ExamResultDetailBO instance = new ExamResultDetailBO();

		protected ExamResultDetailBO()
		{
			this.baseFacade = facade;
		}

		public static ExamResultDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	