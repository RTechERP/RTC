
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PaymentOrderDetailBO : BaseBO
	{
		private PaymentOrderDetailFacade facade = PaymentOrderDetailFacade.Instance;
		protected static PaymentOrderDetailBO instance = new PaymentOrderDetailBO();

		protected PaymentOrderDetailBO()
		{
			this.baseFacade = facade;
		}

		public static PaymentOrderDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	