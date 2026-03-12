
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillExportDocumentBO : BaseBO
	{
		private BillExportDocumentFacade facade = BillExportDocumentFacade.Instance;
		protected static BillExportDocumentBO instance = new BillExportDocumentBO();

		protected BillExportDocumentBO()
		{
			this.baseFacade = facade;
		}

		public static BillExportDocumentBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	