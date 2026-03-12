
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class HistoryProductRTCLogFacade : BaseFacade
	{
		protected static HistoryProductRTCLogFacade instance = new HistoryProductRTCLogFacade(new HistoryProductRTCLogModel());
		protected HistoryProductRTCLogFacade(HistoryProductRTCLogModel model) : base(model)
		{
		}
		public static HistoryProductRTCLogFacade Instance
		{
			get { return instance; }
		}
		protected HistoryProductRTCLogFacade():base() 
		{ 
		} 
	
	}
}
	