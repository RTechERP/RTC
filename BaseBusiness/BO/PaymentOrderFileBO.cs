
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PaymentOrderFileBO : BaseBO
	{
		private PaymentOrderFileFacade facade = PaymentOrderFileFacade.Instance;
		protected static PaymentOrderFileBO instance = new PaymentOrderFileBO();

		protected PaymentOrderFileBO()
		{
			this.baseFacade = facade;
		}

		public static PaymentOrderFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	