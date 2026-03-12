
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestImportDetailBO : BaseBO
	{
		private RequestImportDetailFacade facade = RequestImportDetailFacade.Instance;
		protected static RequestImportDetailBO instance = new RequestImportDetailBO();

		protected RequestImportDetailBO()
		{
			this.baseFacade = facade;
		}

		public static RequestImportDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	