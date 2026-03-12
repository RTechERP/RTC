
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillExportTechDetailSerialBO : BaseBO
	{
		private BillExportTechDetailSerialFacade facade = BillExportTechDetailSerialFacade.Instance;
		protected static BillExportTechDetailSerialBO instance = new BillExportTechDetailSerialBO();

		protected BillExportTechDetailSerialBO()
		{
			this.baseFacade = facade;
		}

		public static BillExportTechDetailSerialBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	