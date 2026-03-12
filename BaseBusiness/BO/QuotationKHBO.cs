
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class QuotationKHBO : BaseBO
	{
		private QuotationKHFacade facade = QuotationKHFacade.Instance;
		protected static QuotationKHBO instance = new QuotationKHBO();

		protected QuotationKHBO()
		{
			this.baseFacade = facade;
		}

		public static QuotationKHBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	