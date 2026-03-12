
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PaymentOrderBO : BaseBO
	{
		private PaymentOrderFacade facade = PaymentOrderFacade.Instance;
		protected static PaymentOrderBO instance = new PaymentOrderBO();

		protected PaymentOrderBO()
		{
			this.baseFacade = facade;
		}

		public static PaymentOrderBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	