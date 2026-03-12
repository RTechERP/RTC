
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSTranferAssetFacade : BaseFacade
	{
		protected static TSTranferAssetFacade instance = new TSTranferAssetFacade(new TSTranferAssetModel());
		protected TSTranferAssetFacade(TSTranferAssetModel model) : base(model)
		{
		}
		public static TSTranferAssetFacade Instance
		{
			get { return instance; }
		}
		protected TSTranferAssetFacade():base() 
		{ 
		} 
	
	}
}
	