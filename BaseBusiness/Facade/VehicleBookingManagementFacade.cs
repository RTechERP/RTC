
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class VehicleBookingManagementFacade : BaseFacade
	{
		protected static VehicleBookingManagementFacade instance = new VehicleBookingManagementFacade(new VehicleBookingManagementModel());
		protected VehicleBookingManagementFacade(VehicleBookingManagementModel model) : base(model)
		{
		}
		public static VehicleBookingManagementFacade Instance
		{
			get { return instance; }
		}
		protected VehicleBookingManagementFacade():base() 
		{ 
		} 
	
	}
}
	