
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DailyReportSaleBO : BaseBO
	{
		private DailyReportSaleFacade facade = DailyReportSaleFacade.Instance;
		protected static DailyReportSaleBO instance = new DailyReportSaleBO();

		protected DailyReportSaleBO()
		{
			this.baseFacade = facade;
		}

		public static DailyReportSaleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	