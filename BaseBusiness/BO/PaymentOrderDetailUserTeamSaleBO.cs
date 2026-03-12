
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class PaymentOrderDetailUserTeamSaleBO : BaseBO
	{
		private PaymentOrderDetailUserTeamSaleFacade facade = PaymentOrderDetailUserTeamSaleFacade.Instance;
		protected static PaymentOrderDetailUserTeamSaleBO instance = new PaymentOrderDetailUserTeamSaleBO();

		protected PaymentOrderDetailUserTeamSaleBO()
		{
			this.baseFacade = facade;
		}

		public static PaymentOrderDetailUserTeamSaleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	