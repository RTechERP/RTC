
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PONCCDetailBO : BaseBO
	{
		private PONCCDetailFacade facade = PONCCDetailFacade.Instance;
		protected static PONCCDetailBO instance = new PONCCDetailBO();

		protected PONCCDetailBO()
		{
			this.baseFacade = facade;
		}

		public static PONCCDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	