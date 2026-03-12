
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class OfficeSupplyRequestsDetailBO : BaseBO
	{
		private OfficeSupplyRequestsDetailFacade facade = OfficeSupplyRequestsDetailFacade.Instance;
		protected static OfficeSupplyRequestsDetailBO instance = new OfficeSupplyRequestsDetailBO();

		protected OfficeSupplyRequestsDetailBO()
		{
			this.baseFacade = facade;
		}

		public static OfficeSupplyRequestsDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	