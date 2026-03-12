
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillDocumentImportLogBO : BaseBO
	{
		private BillDocumentImportLogFacade facade = BillDocumentImportLogFacade.Instance;
		protected static BillDocumentImportLogBO instance = new BillDocumentImportLogBO();

		protected BillDocumentImportLogBO()
		{
			this.baseFacade = facade;
		}

		public static BillDocumentImportLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	