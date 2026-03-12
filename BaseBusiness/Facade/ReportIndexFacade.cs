
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ReportIndexFacade : BaseFacade
	{
		protected static ReportIndexFacade instance = new ReportIndexFacade(new ReportIndexModel());
		protected ReportIndexFacade(ReportIndexModel model) : base(model)
		{
		}
		public static ReportIndexFacade Instance
		{
			get { return instance; }
		}
		protected ReportIndexFacade():base() 
		{ 
		} 
	
	}
}
	