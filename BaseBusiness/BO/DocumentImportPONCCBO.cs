
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DocumentImportPONCCBO : BaseBO
	{
		private DocumentImportPONCCFacade facade = DocumentImportPONCCFacade.Instance;
		protected static DocumentImportPONCCBO instance = new DocumentImportPONCCBO();

		protected DocumentImportPONCCBO()
		{
			this.baseFacade = facade;
		}

		public static DocumentImportPONCCBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	