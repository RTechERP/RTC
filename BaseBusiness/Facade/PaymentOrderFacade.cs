
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PaymentOrderFacade : BaseFacade
	{
		protected static PaymentOrderFacade instance = new PaymentOrderFacade(new PaymentOrderModel());
		protected PaymentOrderFacade(PaymentOrderModel model) : base(model)
		{
		}
		public static PaymentOrderFacade Instance
		{
			get { return instance; }
		}
		protected PaymentOrderFacade():base() 
		{ 
		} 
	
	}
}
	