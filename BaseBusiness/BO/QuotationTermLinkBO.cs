
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class QuotationTermLinkBO : BaseBO
	{
		private QuotationTermLinkFacade facade = QuotationTermLinkFacade.Instance;
		protected static QuotationTermLinkBO instance = new QuotationTermLinkBO();

		protected QuotationTermLinkBO()
		{
			this.baseFacade = facade;
		}

		public static QuotationTermLinkBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	