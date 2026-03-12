
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PaymentOrderCustomerFacade : BaseFacade
	{
		protected static PaymentOrderCustomerFacade instance = new PaymentOrderCustomerFacade(new PaymentOrderCustomerModel());
		protected PaymentOrderCustomerFacade(PaymentOrderCustomerModel model) : base(model)
		{
		}
		public static PaymentOrderCustomerFacade Instance
		{
			get { return instance; }
		}
		protected PaymentOrderCustomerFacade():base() 
		{ 
		} 
	
	}
}
	