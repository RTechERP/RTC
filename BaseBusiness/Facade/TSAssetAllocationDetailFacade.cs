
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSAssetAllocationDetailFacade : BaseFacade
	{
		protected static TSAssetAllocationDetailFacade instance = new TSAssetAllocationDetailFacade(new TSAssetAllocationDetailModel());
		protected TSAssetAllocationDetailFacade(TSAssetAllocationDetailModel model) : base(model)
		{
		}
		public static TSAssetAllocationDetailFacade Instance
		{
			get { return instance; }
		}
		protected TSAssetAllocationDetailFacade():base() 
		{ 
		} 
	
	}
}
	