
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DocumentExportBO : BaseBO
	{
		private DocumentExportFacade facade = DocumentExportFacade.Instance;
		protected static DocumentExportBO instance = new DocumentExportBO();

		protected DocumentExportBO()
		{
			this.baseFacade = facade;
		}

		public static DocumentExportBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	