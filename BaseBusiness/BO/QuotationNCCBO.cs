
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class QuotationNCCBO : BaseBO
	{
		private QuotationNCCFacade facade = QuotationNCCFacade.Instance;
		protected static QuotationNCCBO instance = new QuotationNCCBO();

		protected QuotationNCCBO()
		{
			this.baseFacade = facade;
		}

		public static QuotationNCCBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	