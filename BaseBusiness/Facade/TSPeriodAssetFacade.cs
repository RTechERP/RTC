
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSPeriodAssetFacade : BaseFacade
	{
		protected static TSPeriodAssetFacade instance = new TSPeriodAssetFacade(new TSPeriodAssetModel());
		protected TSPeriodAssetFacade(TSPeriodAssetModel model) : base(model)
		{
		}
		public static TSPeriodAssetFacade Instance
		{
			get { return instance; }
		}
		protected TSPeriodAssetFacade():base() 
		{ 
		} 
	
	}
}
	