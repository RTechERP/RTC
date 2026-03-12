
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class POKHDetailMoneyBO : BaseBO
	{
		private POKHDetailMoneyFacade facade = POKHDetailMoneyFacade.Instance;
		protected static POKHDetailMoneyBO instance = new POKHDetailMoneyBO();

		protected POKHDetailMoneyBO()
		{
			this.baseFacade = facade;
		}

		public static POKHDetailMoneyBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	