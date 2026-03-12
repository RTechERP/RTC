
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSAssetManagementFacade : BaseFacade
	{
		protected static TSAssetManagementFacade instance = new TSAssetManagementFacade(new TSAssetManagementModel());
		protected TSAssetManagementFacade(TSAssetManagementModel model) : base(model)
		{
		}
		public static TSAssetManagementFacade Instance
		{
			get { return instance; }
		}
		protected TSAssetManagementFacade():base() 
		{ 
		} 
	
	}
}
	