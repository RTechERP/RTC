
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PurchaseOrderDetailFacade : BaseFacade
	{
		protected static PurchaseOrderDetailFacade instance = new PurchaseOrderDetailFacade(new PurchaseOrderDetailModel());
		protected PurchaseOrderDetailFacade(PurchaseOrderDetailModel model) : base(model)
		{
		}
		public static PurchaseOrderDetailFacade Instance
		{
			get { return instance; }
		}
		protected PurchaseOrderDetailFacade():base() 
		{ 
		} 
	
	}
}
	