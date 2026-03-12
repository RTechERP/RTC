
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillDocumentImportTechnicalBO : BaseBO
	{
		private BillDocumentImportTechnicalFacade facade = BillDocumentImportTechnicalFacade.Instance;
		protected static BillDocumentImportTechnicalBO instance = new BillDocumentImportTechnicalBO();

		protected BillDocumentImportTechnicalBO()
		{
			this.baseFacade = facade;
		}

		public static BillDocumentImportTechnicalBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	