
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class QuotationDetailBO : BaseBO
	{
		private QuotationDetailFacade facade = QuotationDetailFacade.Instance;
		protected static QuotationDetailBO instance = new QuotationDetailBO();

		protected QuotationDetailBO()
		{
			this.baseFacade = facade;
		}

		public static QuotationDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	