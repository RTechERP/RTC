
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PaymentOrderTypeFacade : BaseFacade
	{
		protected static PaymentOrderTypeFacade instance = new PaymentOrderTypeFacade(new PaymentOrderTypeModel());
		protected PaymentOrderTypeFacade(PaymentOrderTypeModel model) : base(model)
		{
		}
		public static PaymentOrderTypeFacade Instance
		{
			get { return instance; }
		}
		protected PaymentOrderTypeFacade():base() 
		{ 
		} 
	
	}
}
	