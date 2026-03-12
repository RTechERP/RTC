
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillDocumentImportBO : BaseBO
	{
		private BillDocumentImportFacade facade = BillDocumentImportFacade.Instance;
		protected static BillDocumentImportBO instance = new BillDocumentImportBO();

		protected BillDocumentImportBO()
		{
			this.baseFacade = facade;
		}

		public static BillDocumentImportBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	