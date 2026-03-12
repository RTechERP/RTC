
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class VehicleBookingManagementBO : BaseBO
	{
		private VehicleBookingManagementFacade facade = VehicleBookingManagementFacade.Instance;
		protected static VehicleBookingManagementBO instance = new VehicleBookingManagementBO();

		protected VehicleBookingManagementBO()
		{
			this.baseFacade = facade;
		}

		public static VehicleBookingManagementBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	