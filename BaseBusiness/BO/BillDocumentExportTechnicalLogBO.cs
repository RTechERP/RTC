
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillDocumentExportTechnicalLogBO : BaseBO
	{
		private BillDocumentExportTechnicalLogFacade facade = BillDocumentExportTechnicalLogFacade.Instance;
		protected static BillDocumentExportTechnicalLogBO instance = new BillDocumentExportTechnicalLogBO();

		protected BillDocumentExportTechnicalLogBO()
		{
			this.baseFacade = facade;
		}

		public static BillDocumentExportTechnicalLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	