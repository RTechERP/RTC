
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class OfficeSupplyBO : BaseBO
	{
		private OfficeSupplyFacade facade = OfficeSupplyFacade.Instance;
		protected static OfficeSupplyBO instance = new OfficeSupplyBO();

		protected OfficeSupplyBO()
		{
			this.baseFacade = facade;
		}

		public static OfficeSupplyBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	