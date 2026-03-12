
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class VehicleBookingFileBO : BaseBO
	{
		private VehicleBookingFileFacade facade = VehicleBookingFileFacade.Instance;
		protected static VehicleBookingFileBO instance = new VehicleBookingFileBO();

		protected VehicleBookingFileBO()
		{
			this.baseFacade = facade;
		}

		public static VehicleBookingFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	