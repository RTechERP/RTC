
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DeliveryProgressFacade : BaseFacade
	{
		protected static DeliveryProgressFacade instance = new DeliveryProgressFacade(new DeliveryProgressModel());
		protected DeliveryProgressFacade(DeliveryProgressModel model) : base(model)
		{
		}
		public static DeliveryProgressFacade Instance
		{
			get { return instance; }
		}
		protected DeliveryProgressFacade():base() 
		{ 
		} 
	
	}
}
	