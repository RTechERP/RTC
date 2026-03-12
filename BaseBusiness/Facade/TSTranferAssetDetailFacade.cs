
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSTranferAssetDetailFacade : BaseFacade
	{
		protected static TSTranferAssetDetailFacade instance = new TSTranferAssetDetailFacade(new TSTranferAssetDetailModel());
		protected TSTranferAssetDetailFacade(TSTranferAssetDetailModel model) : base(model)
		{
		}
		public static TSTranferAssetDetailFacade Instance
		{
			get { return instance; }
		}
		protected TSTranferAssetDetailFacade():base() 
		{ 
		} 
	
	}
}
	