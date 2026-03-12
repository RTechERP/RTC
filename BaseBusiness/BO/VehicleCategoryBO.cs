
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class VehicleCategoryBO : BaseBO
	{
		private VehicleCategoryFacade facade = VehicleCategoryFacade.Instance;
		protected static VehicleCategoryBO instance = new VehicleCategoryBO();

		protected VehicleCategoryBO()
		{
			this.baseFacade = facade;
		}

		public static VehicleCategoryBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	