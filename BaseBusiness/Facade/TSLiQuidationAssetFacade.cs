
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSLiQuidationAssetFacade : BaseFacade
	{
		protected static TSLiQuidationAssetFacade instance = new TSLiQuidationAssetFacade(new TSLiQuidationAssetModel());
		protected TSLiQuidationAssetFacade(TSLiQuidationAssetModel model) : base(model)
		{
		}
		public static TSLiQuidationAssetFacade Instance
		{
			get { return instance; }
		}
		protected TSLiQuidationAssetFacade():base() 
		{ 
		} 
	
	}
}
	