
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class OfficeSupplyRequestBO : BaseBO
	{
		private OfficeSupplyRequestFacade facade = OfficeSupplyRequestFacade.Instance;
		protected static OfficeSupplyRequestBO instance = new OfficeSupplyRequestBO();

		protected OfficeSupplyRequestBO()
		{
			this.baseFacade = facade;
		}

		public static OfficeSupplyRequestBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	