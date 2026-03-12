
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PurchaseOrderBO : BaseBO
	{
		private PurchaseOrderFacade facade = PurchaseOrderFacade.Instance;
		protected static PurchaseOrderBO instance = new PurchaseOrderBO();

		protected PurchaseOrderBO()
		{
			this.baseFacade = facade;
		}

		public static PurchaseOrderBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	