
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DocumentImportBO : BaseBO
	{
		private DocumentImportFacade facade = DocumentImportFacade.Instance;
		protected static DocumentImportBO instance = new DocumentImportBO();

		protected DocumentImportBO()
		{
			this.baseFacade = facade;
		}

		public static DocumentImportBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	