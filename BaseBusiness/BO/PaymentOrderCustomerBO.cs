
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PaymentOrderCustomerBO : BaseBO
	{
		private PaymentOrderCustomerFacade facade = PaymentOrderCustomerFacade.Instance;
		protected static PaymentOrderCustomerBO instance = new PaymentOrderCustomerBO();

		protected PaymentOrderCustomerBO()
		{
			this.baseFacade = facade;
		}

		public static PaymentOrderCustomerBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	