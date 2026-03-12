
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PaymentOrderDetailUserTeamSaleFacade : BaseFacade
	{
		protected static PaymentOrderDetailUserTeamSaleFacade instance = new PaymentOrderDetailUserTeamSaleFacade(new PaymentOrderDetailUserTeamSaleModel());
		protected PaymentOrderDetailUserTeamSaleFacade(PaymentOrderDetailUserTeamSaleModel model) : base(model)
		{
		}
		public static PaymentOrderDetailUserTeamSaleFacade Instance
		{
			get { return instance; }
		}
		protected PaymentOrderDetailUserTeamSaleFacade():base() 
		{ 
		} 
	
	}
}
	