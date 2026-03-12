
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class InvoiceLinkBO : BaseBO
	{
		private InvoiceLinkFacade facade = InvoiceLinkFacade.Instance;
		protected static InvoiceLinkBO instance = new InvoiceLinkBO();

		protected InvoiceLinkBO()
		{
			this.baseFacade = facade;
		}

		public static InvoiceLinkBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	