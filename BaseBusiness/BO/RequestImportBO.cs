
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestImportBO : BaseBO
	{
		private RequestImportFacade facade = RequestImportFacade.Instance;
		protected static RequestImportBO instance = new RequestImportBO();

		protected RequestImportBO()
		{
			this.baseFacade = facade;
		}

		public static RequestImportBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	