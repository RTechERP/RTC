
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillImportTechDetailSerialBO : BaseBO
	{
		private BillImportTechDetailSerialFacade facade = BillImportTechDetailSerialFacade.Instance;
		protected static BillImportTechDetailSerialBO instance = new BillImportTechDetailSerialBO();

		protected BillImportTechDetailSerialBO()
		{
			this.baseFacade = facade;
		}

		public static BillImportTechDetailSerialBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	