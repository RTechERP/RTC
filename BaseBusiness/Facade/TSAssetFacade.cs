
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSAssetFacade : BaseFacade
	{
		protected static TSAssetFacade instance = new TSAssetFacade(new TSAssetModel());
		protected TSAssetFacade(TSAssetModel model) : base(model)
		{
		}
		public static TSAssetFacade Instance
		{
			get { return instance; }
		}
		protected TSAssetFacade():base() 
		{ 
		} 
	
	}
}
	