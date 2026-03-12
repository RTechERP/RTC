
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamListTestBO : BaseBO
	{
		private ExamListTestFacade facade = ExamListTestFacade.Instance;
		protected static ExamListTestBO instance = new ExamListTestBO();

		protected ExamListTestBO()
		{
			this.baseFacade = facade;
		}

		public static ExamListTestBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	