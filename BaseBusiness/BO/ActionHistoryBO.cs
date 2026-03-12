
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ActionHistoryBO : BaseBO
	{
		private ActionHistoryFacade facade = ActionHistoryFacade.Instance;
		protected static ActionHistoryBO instance = new ActionHistoryBO();

		protected ActionHistoryBO()
		{
			this.baseFacade = facade;
		}

		public static ActionHistoryBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	