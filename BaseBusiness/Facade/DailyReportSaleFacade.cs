
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DailyReportSaleFacade : BaseFacade
	{
		protected static DailyReportSaleFacade instance = new DailyReportSaleFacade(new DailyReportSaleModel());
		protected DailyReportSaleFacade(DailyReportSaleModel model) : base(model)
		{
		}
		public static DailyReportSaleFacade Instance
		{
			get { return instance; }
		}
		protected DailyReportSaleFacade():base() 
		{ 
		} 
	
	}
}
	