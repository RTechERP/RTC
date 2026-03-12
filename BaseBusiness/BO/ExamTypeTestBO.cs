
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ExamTypeTestBO : BaseBO
	{
		private ExamTypeTestFacade facade = ExamTypeTestFacade.Instance;
		protected static ExamTypeTestBO instance = new ExamTypeTestBO();

		protected ExamTypeTestBO()
		{
			this.baseFacade = facade;
		}

		public static ExamTypeTestBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	