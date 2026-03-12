
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSCalendarPeriodAssetFacade : BaseFacade
	{
		protected static TSCalendarPeriodAssetFacade instance = new TSCalendarPeriodAssetFacade(new TSCalendarPeriodAssetModel());
		protected TSCalendarPeriodAssetFacade(TSCalendarPeriodAssetModel model) : base(model)
		{
		}
		public static TSCalendarPeriodAssetFacade Instance
		{
			get { return instance; }
		}
		protected TSCalendarPeriodAssetFacade():base() 
		{ 
		} 
	
	}
}
	