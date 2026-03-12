
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class OfficeSupplyRequestsBO : BaseBO
	{
		private OfficeSupplyRequestsFacade facade = OfficeSupplyRequestsFacade.Instance;
		protected static OfficeSupplyRequestsBO instance = new OfficeSupplyRequestsBO();

		protected OfficeSupplyRequestsBO()
		{
			this.baseFacade = facade;
		}

		public static OfficeSupplyRequestsBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	