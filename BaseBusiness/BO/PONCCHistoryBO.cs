
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PONCCHistoryBO : BaseBO
	{
		private PONCCHistoryFacade facade = PONCCHistoryFacade.Instance;
		protected static PONCCHistoryBO instance = new PONCCHistoryBO();

		protected PONCCHistoryBO()
		{
			this.baseFacade = facade;
		}

		public static PONCCHistoryBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	