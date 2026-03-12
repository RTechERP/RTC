
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PaymentOrderLogFacade : BaseFacade
	{
		protected static PaymentOrderLogFacade instance = new PaymentOrderLogFacade(new PaymentOrderLogModel());
		protected PaymentOrderLogFacade(PaymentOrderLogModel model) : base(model)
		{
		}
		public static PaymentOrderLogFacade Instance
		{
			get { return instance; }
		}
		protected PaymentOrderLogFacade():base() 
		{ 
		} 
	
	}
}
	