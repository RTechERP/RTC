
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DailyReportSaleAdminFacade : BaseFacade
	{
		protected static DailyReportSaleAdminFacade instance = new DailyReportSaleAdminFacade(new DailyReportSaleAdminModel());
		protected DailyReportSaleAdminFacade(DailyReportSaleAdminModel model) : base(model)
		{
		}
		public static DailyReportSaleAdminFacade Instance
		{
			get { return instance; }
		}
		protected DailyReportSaleAdminFacade():base() 
		{ 
		} 
	
	}
}
	