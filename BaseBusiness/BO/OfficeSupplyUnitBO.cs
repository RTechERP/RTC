
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class OfficeSupplyUnitBO : BaseBO
	{
		private OfficeSupplyUnitFacade facade = OfficeSupplyUnitFacade.Instance;
		protected static OfficeSupplyUnitBO instance = new OfficeSupplyUnitBO();

		protected OfficeSupplyUnitBO()
		{
			this.baseFacade = facade;
		}

		public static OfficeSupplyUnitBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	