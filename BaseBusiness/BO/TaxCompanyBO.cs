
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TaxCompanyBO : BaseBO
	{
		private TaxCompanyFacade facade = TaxCompanyFacade.Instance;
		protected static TaxCompanyBO instance = new TaxCompanyBO();

		protected TaxCompanyBO()
		{
			this.baseFacade = facade;
		}

		public static TaxCompanyBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	