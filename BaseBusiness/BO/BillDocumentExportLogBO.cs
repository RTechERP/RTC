
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillDocumentExportLogBO : BaseBO
	{
		private BillDocumentExportLogFacade facade = BillDocumentExportLogFacade.Instance;
		protected static BillDocumentExportLogBO instance = new BillDocumentExportLogBO();

		protected BillDocumentExportLogBO()
		{
			this.baseFacade = facade;
		}

		public static BillDocumentExportLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	