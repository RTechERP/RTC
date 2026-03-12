
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PurchaseOrderDetailBO : BaseBO
	{
		private PurchaseOrderDetailFacade facade = PurchaseOrderDetailFacade.Instance;
		protected static PurchaseOrderDetailBO instance = new PurchaseOrderDetailBO();

		protected PurchaseOrderDetailBO()
		{
			this.baseFacade = facade;
		}

		public static PurchaseOrderDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	