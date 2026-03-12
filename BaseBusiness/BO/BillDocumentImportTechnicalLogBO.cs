
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillDocumentImportTechnicalLogBO : BaseBO
	{
		private BillDocumentImportTechnicalLogFacade facade = BillDocumentImportTechnicalLogFacade.Instance;
		protected static BillDocumentImportTechnicalLogBO instance = new BillDocumentImportTechnicalLogBO();

		protected BillDocumentImportTechnicalLogBO()
		{
			this.baseFacade = facade;
		}

		public static BillDocumentImportTechnicalLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	