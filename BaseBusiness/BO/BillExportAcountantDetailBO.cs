
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillExportAcountantDetailBO : BaseBO
	{
		private BillExportAcountantDetailFacade facade = BillExportAcountantDetailFacade.Instance;
		protected static BillExportAcountantDetailBO instance = new BillExportAcountantDetailBO();

		protected BillExportAcountantDetailBO()
		{
			this.baseFacade = facade;
		}

		public static BillExportAcountantDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	