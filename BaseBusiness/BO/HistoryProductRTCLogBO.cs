
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class HistoryProductRTCLogBO : BaseBO
	{
		private HistoryProductRTCLogFacade facade = HistoryProductRTCLogFacade.Instance;
		protected static HistoryProductRTCLogBO instance = new HistoryProductRTCLogBO();

		protected HistoryProductRTCLogBO()
		{
			this.baseFacade = facade;
		}

		public static HistoryProductRTCLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	