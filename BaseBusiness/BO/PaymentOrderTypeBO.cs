
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PaymentOrderTypeBO : BaseBO
	{
		private PaymentOrderTypeFacade facade = PaymentOrderTypeFacade.Instance;
		protected static PaymentOrderTypeBO instance = new PaymentOrderTypeBO();

		protected PaymentOrderTypeBO()
		{
			this.baseFacade = facade;
		}

		public static PaymentOrderTypeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	