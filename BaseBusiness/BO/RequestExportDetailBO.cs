
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestExportDetailBO : BaseBO
	{
		private RequestExportDetailFacade facade = RequestExportDetailFacade.Instance;
		protected static RequestExportDetailBO instance = new RequestExportDetailBO();

		protected RequestExportDetailBO()
		{
			this.baseFacade = facade;
		}

		public static RequestExportDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	