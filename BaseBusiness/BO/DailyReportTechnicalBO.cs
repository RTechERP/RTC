
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DailyReportTechnicalBO : BaseBO
	{
		private DailyReportTechnicalFacade facade = DailyReportTechnicalFacade.Instance;
		protected static DailyReportTechnicalBO instance = new DailyReportTechnicalBO();

		protected DailyReportTechnicalBO()
		{
			this.baseFacade = facade;
		}

		public static DailyReportTechnicalBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	