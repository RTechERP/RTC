
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TrackingMarksTaxCompanyFacade : BaseFacade
	{
		protected static TrackingMarksTaxCompanyFacade instance = new TrackingMarksTaxCompanyFacade(new TrackingMarksTaxCompanyModel());
		protected TrackingMarksTaxCompanyFacade(TrackingMarksTaxCompanyModel model) : base(model)
		{
		}
		public static TrackingMarksTaxCompanyFacade Instance
		{
			get { return instance; }
		}
		protected TrackingMarksTaxCompanyFacade():base() 
		{ 
		} 
	
	}
}
	