
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TSLostReportAssetFacade : BaseFacade
	{
		protected static TSLostReportAssetFacade instance = new TSLostReportAssetFacade(new TSLostReportAssetModel());
		protected TSLostReportAssetFacade(TSLostReportAssetModel model) : base(model)
		{
		}
		public static TSLostReportAssetFacade Instance
		{
			get { return instance; }
		}
		protected TSLostReportAssetFacade():base() 
		{ 
		} 
	
	}
}
	