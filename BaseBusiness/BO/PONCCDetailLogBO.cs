
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PONCCDetailLogBO : BaseBO
	{
		private PONCCDetailLogFacade facade = PONCCDetailLogFacade.Instance;
		protected static PONCCDetailLogBO instance = new PONCCDetailLogBO();

		protected PONCCDetailLogBO()
		{
			this.baseFacade = facade;
		}

		public static PONCCDetailLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	