
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class POKHFileBO : BaseBO
	{
		private POKHFileFacade facade = POKHFileFacade.Instance;
		protected static POKHFileBO instance = new POKHFileBO();

		protected POKHFileBO()
		{
			this.baseFacade = facade;
		}

		public static POKHFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	