
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PaymentOrderLogBO : BaseBO
	{
		private PaymentOrderLogFacade facade = PaymentOrderLogFacade.Instance;
		protected static PaymentOrderLogBO instance = new PaymentOrderLogBO();

		protected PaymentOrderLogBO()
		{
			this.baseFacade = facade;
		}

		public static PaymentOrderLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	