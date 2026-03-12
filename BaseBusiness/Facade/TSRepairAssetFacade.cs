
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSRepairAssetFacade : BaseFacade
	{
		protected static TSRepairAssetFacade instance = new TSRepairAssetFacade(new TSRepairAssetModel());
		protected TSRepairAssetFacade(TSRepairAssetModel model) : base(model)
		{
		}
		public static TSRepairAssetFacade Instance
		{
			get { return instance; }
		}
		protected TSRepairAssetFacade():base() 
		{ 
		} 
	
	}
}
	