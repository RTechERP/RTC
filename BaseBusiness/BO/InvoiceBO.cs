
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class InvoiceBO : BaseBO
	{
		private InvoiceFacade facade = InvoiceFacade.Instance;
		protected static InvoiceBO instance = new InvoiceBO();

		protected InvoiceBO()
		{
			this.baseFacade = facade;
		}

		public static InvoiceBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	