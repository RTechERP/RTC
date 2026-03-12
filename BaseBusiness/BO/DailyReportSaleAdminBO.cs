
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DailyReportSaleAdminBO : BaseBO
	{
		private DailyReportSaleAdminFacade facade = DailyReportSaleAdminFacade.Instance;
		protected static DailyReportSaleAdminBO instance = new DailyReportSaleAdminBO();

		protected DailyReportSaleAdminBO()
		{
			this.baseFacade = facade;
		}

		public static DailyReportSaleAdminBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	