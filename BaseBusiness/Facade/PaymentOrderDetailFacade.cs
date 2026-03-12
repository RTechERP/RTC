
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PaymentOrderDetailFacade : BaseFacade
	{
		protected static PaymentOrderDetailFacade instance = new PaymentOrderDetailFacade(new PaymentOrderDetailModel());
		protected PaymentOrderDetailFacade(PaymentOrderDetailModel model) : base(model)
		{
		}
		public static PaymentOrderDetailFacade Instance
		{
			get { return instance; }
		}
		protected PaymentOrderDetailFacade():base() 
		{ 
		} 
	
	}
}
	