
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestExportBO : BaseBO
	{
		private RequestExportFacade facade = RequestExportFacade.Instance;
		protected static RequestExportBO instance = new RequestExportBO();

		protected RequestExportBO()
		{
			this.baseFacade = facade;
		}

		public static RequestExportBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	