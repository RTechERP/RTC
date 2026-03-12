
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSAllocationEvictionAssetFacade : BaseFacade
	{
		protected static TSAllocationEvictionAssetFacade instance = new TSAllocationEvictionAssetFacade(new TSAllocationEvictionAssetModel());
		protected TSAllocationEvictionAssetFacade(TSAllocationEvictionAssetModel model) : base(model)
		{
		}
		public static TSAllocationEvictionAssetFacade Instance
		{
			get { return instance; }
		}
		protected TSAllocationEvictionAssetFacade():base() 
		{ 
		} 
	
	}
}
	