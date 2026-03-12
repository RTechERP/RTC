
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillImportDocumentBO : BaseBO
	{
		private BillImportDocumentFacade facade = BillImportDocumentFacade.Instance;
		protected static BillImportDocumentBO instance = new BillImportDocumentBO();

		protected BillImportDocumentBO()
		{
			this.baseFacade = facade;
		}

		public static BillImportDocumentBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	