
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class QuotationNCCDetailBO : BaseBO
	{
		private QuotationNCCDetailFacade facade = QuotationNCCDetailFacade.Instance;
		protected static QuotationNCCDetailBO instance = new QuotationNCCDetailBO();

		protected QuotationNCCDetailBO()
		{
			this.baseFacade = facade;
		}

		public static QuotationNCCDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	