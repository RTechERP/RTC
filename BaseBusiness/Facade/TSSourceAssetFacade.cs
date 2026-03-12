
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSSourceAssetFacade : BaseFacade
	{
		protected static TSSourceAssetFacade instance = new TSSourceAssetFacade(new TSSourceAssetModel());
		protected TSSourceAssetFacade(TSSourceAssetModel model) : base(model)
		{
		}
		public static TSSourceAssetFacade Instance
		{
			get { return instance; }
		}
		protected TSSourceAssetFacade():base() 
		{ 
		} 
	
	}
}
	