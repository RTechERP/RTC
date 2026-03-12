
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class HistoryProductRTCFacade : BaseFacade
	{
		protected static HistoryProductRTCFacade instance = new HistoryProductRTCFacade(new HistoryProductRTCModel());
		protected HistoryProductRTCFacade(HistoryProductRTCModel model) : base(model)
		{
		}
		public static HistoryProductRTCFacade Instance
		{
			get { return instance; }
		}
		protected HistoryProductRTCFacade():base() 
		{ 
		} 
	
	}
}
	