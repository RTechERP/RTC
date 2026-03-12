
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TrackingMarksTaxCompanyBO : BaseBO
	{
		private TrackingMarksTaxCompanyFacade facade = TrackingMarksTaxCompanyFacade.Instance;
		protected static TrackingMarksTaxCompanyBO instance = new TrackingMarksTaxCompanyBO();

		protected TrackingMarksTaxCompanyBO()
		{
			this.baseFacade = facade;
		}

		public static TrackingMarksTaxCompanyBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	