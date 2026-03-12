
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSAssetRecoveryFacade : BaseFacade
	{
		protected static TSAssetRecoveryFacade instance = new TSAssetRecoveryFacade(new TSAssetRecoveryModel());
		protected TSAssetRecoveryFacade(TSAssetRecoveryModel model) : base(model)
		{
		}
		public static TSAssetRecoveryFacade Instance
		{
			get { return instance; }
		}
		protected TSAssetRecoveryFacade():base() 
		{ 
		} 
	
	}
}
	