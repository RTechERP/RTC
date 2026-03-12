
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BusinessFieldLinkBO : BaseBO
	{
		private BusinessFieldLinkFacade facade = BusinessFieldLinkFacade.Instance;
		protected static BusinessFieldLinkBO instance = new BusinessFieldLinkBO();

		protected BusinessFieldLinkBO()
		{
			this.baseFacade = facade;
		}

		public static BusinessFieldLinkBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	