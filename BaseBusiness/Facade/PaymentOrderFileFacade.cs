
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PaymentOrderFileFacade : BaseFacade
	{
		protected static PaymentOrderFileFacade instance = new PaymentOrderFileFacade(new PaymentOrderFileModel());
		protected PaymentOrderFileFacade(PaymentOrderFileModel model) : base(model)
		{
		}
		public static PaymentOrderFileFacade Instance
		{
			get { return instance; }
		}
		protected PaymentOrderFileFacade():base() 
		{ 
		} 
	
	}
}
	