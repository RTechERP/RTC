
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSAssetManagementHistoryFacade : BaseFacade
	{
		protected static TSAssetManagementHistoryFacade instance = new TSAssetManagementHistoryFacade(new TSAssetManagementHistoryModel());
		protected TSAssetManagementHistoryFacade(TSAssetManagementHistoryModel model) : base(model)
		{
		}
		public static TSAssetManagementHistoryFacade Instance
		{
			get { return instance; }
		}
		protected TSAssetManagementHistoryFacade():base() 
		{ 
		} 
	
	}
}
	