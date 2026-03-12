
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSReportBrokenAssetFacade : BaseFacade
	{
		protected static TSReportBrokenAssetFacade instance = new TSReportBrokenAssetFacade(new TSReportBrokenAssetModel());
		protected TSReportBrokenAssetFacade(TSReportBrokenAssetModel model) : base(model)
		{
		}
		public static TSReportBrokenAssetFacade Instance
		{
			get { return instance; }
		}
		protected TSReportBrokenAssetFacade():base() 
		{ 
		} 
	
	}
}
	