
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DocumentFileBO : BaseBO
	{
		private DocumentFileFacade facade = DocumentFileFacade.Instance;
		protected static DocumentFileBO instance = new DocumentFileBO();

		protected DocumentFileBO()
		{
			this.baseFacade = facade;
		}

		public static DocumentFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	