
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class QuotationKHDetailBO : BaseBO
	{
		private QuotationKHDetailFacade facade = QuotationKHDetailFacade.Instance;
		protected static QuotationKHDetailBO instance = new QuotationKHDetailBO();

		protected QuotationKHDetailBO()
		{
			this.baseFacade = facade;
		}

		public static QuotationKHDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	