
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DocumentBO : BaseBO
	{
		private DocumentFacade facade = DocumentFacade.Instance;
		protected static DocumentBO instance = new DocumentBO();

		protected DocumentBO()
		{
			this.baseFacade = facade;
		}

		public static DocumentBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	