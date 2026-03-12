
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DocumentSaleBO : BaseBO
	{
		private DocumentSaleFacade facade = DocumentSaleFacade.Instance;
		protected static DocumentSaleBO instance = new DocumentSaleBO();

		protected DocumentSaleBO()
		{
			this.baseFacade = facade;
		}

		public static DocumentSaleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	