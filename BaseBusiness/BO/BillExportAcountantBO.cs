
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillExportAcountantBO : BaseBO
	{
		private BillExportAcountantFacade facade = BillExportAcountantFacade.Instance;
		protected static BillExportAcountantBO instance = new BillExportAcountantBO();

		protected BillExportAcountantBO()
		{
			this.baseFacade = facade;
		}

		public static BillExportAcountantBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	