
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DailyReportHRBO : BaseBO
	{
		private DailyReportHRFacade facade = DailyReportHRFacade.Instance;
		protected static DailyReportHRBO instance = new DailyReportHRBO();

		protected DailyReportHRBO()
		{
			this.baseFacade = facade;
		}

		public static DailyReportHRBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	