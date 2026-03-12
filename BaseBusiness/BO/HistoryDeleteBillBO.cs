
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class HistoryDeleteBillBO : BaseBO
	{
		private HistoryDeleteBillFacade facade = HistoryDeleteBillFacade.Instance;
		protected static HistoryDeleteBillBO instance = new HistoryDeleteBillBO();

		protected HistoryDeleteBillBO()
		{
			this.baseFacade = facade;
		}

		public static HistoryDeleteBillBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	