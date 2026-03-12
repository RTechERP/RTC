
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class HistoryKPISaleBO : BaseBO
	{
		private HistoryKPISaleFacade facade = HistoryKPISaleFacade.Instance;
		protected static HistoryKPISaleBO instance = new HistoryKPISaleBO();

		protected HistoryKPISaleBO()
		{
			this.baseFacade = facade;
		}

		public static HistoryKPISaleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	