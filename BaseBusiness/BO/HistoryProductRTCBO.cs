
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class HistoryProductRTCBO : BaseBO
	{
		private HistoryProductRTCFacade facade = HistoryProductRTCFacade.Instance;
		protected static HistoryProductRTCBO instance = new HistoryProductRTCBO();

		protected HistoryProductRTCBO()
		{
			this.baseFacade = facade;
		}

		public static HistoryProductRTCBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	