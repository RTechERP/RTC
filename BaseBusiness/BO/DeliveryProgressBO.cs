
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DeliveryProgressBO : BaseBO
	{
		private DeliveryProgressFacade facade = DeliveryProgressFacade.Instance;
		protected static DeliveryProgressBO instance = new DeliveryProgressBO();

		protected DeliveryProgressBO()
		{
			this.baseFacade = facade;
		}

		public static DeliveryProgressBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	