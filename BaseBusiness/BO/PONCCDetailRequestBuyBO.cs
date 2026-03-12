
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PONCCDetailRequestBuyBO : BaseBO
	{
		private PONCCDetailRequestBuyFacade facade = PONCCDetailRequestBuyFacade.Instance;
		protected static PONCCDetailRequestBuyBO instance = new PONCCDetailRequestBuyBO();

		protected PONCCDetailRequestBuyBO()
		{
			this.baseFacade = facade;
		}

		public static PONCCDetailRequestBuyBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	