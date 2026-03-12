
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class POKHDetailBO : BaseBO
	{
		private POKHDetailFacade facade = POKHDetailFacade.Instance;
		protected static POKHDetailBO instance = new POKHDetailBO();

		protected POKHDetailBO()
		{
			this.baseFacade = facade;
		}

		public static POKHDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	