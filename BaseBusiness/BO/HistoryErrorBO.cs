
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class HistoryErrorBO : BaseBO
	{
		private HistoryErrorFacade facade = HistoryErrorFacade.Instance;
		protected static HistoryErrorBO instance = new HistoryErrorBO();

		protected HistoryErrorBO()
		{
			this.baseFacade = facade;
		}

		public static HistoryErrorBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	