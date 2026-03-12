
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DailyReportHRFacade : BaseFacade
	{
		protected static DailyReportHRFacade instance = new DailyReportHRFacade(new DailyReportHRModel());
		protected DailyReportHRFacade(DailyReportHRModel model) : base(model)
		{
		}
		public static DailyReportHRFacade Instance
		{
			get { return instance; }
		}
		protected DailyReportHRFacade():base() 
		{ 
		} 
	
	}
}
	