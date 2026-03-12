
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PurchaseOrderFacade : BaseFacade
	{
		protected static PurchaseOrderFacade instance = new PurchaseOrderFacade(new PurchaseOrderModel());
		protected PurchaseOrderFacade(PurchaseOrderModel model) : base(model)
		{
		}
		public static PurchaseOrderFacade Instance
		{
			get { return instance; }
		}
		protected PurchaseOrderFacade():base() 
		{ 
		} 
	
	}
}
	