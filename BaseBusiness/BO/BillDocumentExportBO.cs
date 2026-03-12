
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillDocumentExportBO : BaseBO
	{
		private BillDocumentExportFacade facade = BillDocumentExportFacade.Instance;
		protected static BillDocumentExportBO instance = new BillDocumentExportBO();

		protected BillDocumentExportBO()
		{
			this.baseFacade = facade;
		}

		public static BillDocumentExportBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	