
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSAssetRecoveryDetailFacade : BaseFacade
	{
		protected static TSAssetRecoveryDetailFacade instance = new TSAssetRecoveryDetailFacade(new TSAssetRecoveryDetailModel());
		protected TSAssetRecoveryDetailFacade(TSAssetRecoveryDetailModel model) : base(model)
		{
		}
		public static TSAssetRecoveryDetailFacade Instance
		{
			get { return instance; }
		}
		protected TSAssetRecoveryDetailFacade():base() 
		{ 
		} 
	
	}
}
	