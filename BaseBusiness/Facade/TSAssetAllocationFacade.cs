
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSAssetAllocationFacade : BaseFacade
	{
		protected static TSAssetAllocationFacade instance = new TSAssetAllocationFacade(new TSAssetAllocationModel());
		protected TSAssetAllocationFacade(TSAssetAllocationModel model) : base(model)
		{
		}
		public static TSAssetAllocationFacade Instance
		{
			get { return instance; }
		}
		protected TSAssetAllocationFacade():base() 
		{ 
		} 
	
	}
}
	