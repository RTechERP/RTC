
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillDocumentExportTechnicalBO : BaseBO
	{
		private BillDocumentExportTechnicalFacade facade = BillDocumentExportTechnicalFacade.Instance;
		protected static BillDocumentExportTechnicalBO instance = new BillDocumentExportTechnicalBO();

		protected BillDocumentExportTechnicalBO()
		{
			this.baseFacade = facade;
		}

		public static BillDocumentExportTechnicalBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	