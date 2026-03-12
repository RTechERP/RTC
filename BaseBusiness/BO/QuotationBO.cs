
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class QuotationBO : BaseBO
	{
		private QuotationFacade facade = QuotationFacade.Instance;
		protected static QuotationBO instance = new QuotationBO();

		protected QuotationBO()
		{
			this.baseFacade = facade;
		}

		public static QuotationBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	