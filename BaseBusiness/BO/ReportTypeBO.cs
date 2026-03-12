
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ReportTypeBO : BaseBO
	{
		private ReportTypeFacade facade = ReportTypeFacade.Instance;
		protected static ReportTypeBO instance = new ReportTypeBO();

		protected ReportTypeBO()
		{
			this.baseFacade = facade;
		}

		public static ReportTypeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	