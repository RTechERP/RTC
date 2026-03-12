
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DailyReportTechnicalFacade : BaseFacade
	{
		protected static DailyReportTechnicalFacade instance = new DailyReportTechnicalFacade(new DailyReportTechnicalModel());
		protected DailyReportTechnicalFacade(DailyReportTechnicalModel model) : base(model)
		{
		}
		public static DailyReportTechnicalFacade Instance
		{
			get { return instance; }
		}
		protected DailyReportTechnicalFacade():base() 
		{ 
		} 
	
	}
}
	