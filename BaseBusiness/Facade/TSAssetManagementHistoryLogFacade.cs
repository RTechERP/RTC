
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSAssetManagementHistoryLogFacade : BaseFacade
	{
		protected static TSAssetManagementHistoryLogFacade instance = new TSAssetManagementHistoryLogFacade(new TSAssetManagementHistoryLogModel());
		protected TSAssetManagementHistoryLogFacade(TSAssetManagementHistoryLogModel model) : base(model)
		{
		}
		public static TSAssetManagementHistoryLogFacade Instance
		{
			get { return instance; }
		}
		protected TSAssetManagementHistoryLogFacade():base() 
		{ 
		} 
	
	}
}
	