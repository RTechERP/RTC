
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSStatusAssetFacade : BaseFacade
	{
		protected static TSStatusAssetFacade instance = new TSStatusAssetFacade(new TSStatusAssetModel());
		protected TSStatusAssetFacade(TSStatusAssetModel model) : base(model)
		{
		}
		public static TSStatusAssetFacade Instance
		{
			get { return instance; }
		}
		protected TSStatusAssetFacade():base() 
		{ 
		} 
	
	}
}
	