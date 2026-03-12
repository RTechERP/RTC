
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DocumentTypeBO : BaseBO
	{
		private DocumentTypeFacade facade = DocumentTypeFacade.Instance;
		protected static DocumentTypeBO instance = new DocumentTypeBO();

		protected DocumentTypeBO()
		{
			this.baseFacade = facade;
		}

		public static DocumentTypeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	