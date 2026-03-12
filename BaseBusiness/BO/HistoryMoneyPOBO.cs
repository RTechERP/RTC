
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class HistoryMoneyPOBO : BaseBO
	{
		private HistoryMoneyPOFacade facade = HistoryMoneyPOFacade.Instance;
		protected static HistoryMoneyPOBO instance = new HistoryMoneyPOBO();

		protected HistoryMoneyPOBO()
		{
			this.baseFacade = facade;
		}

		public static HistoryMoneyPOBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	