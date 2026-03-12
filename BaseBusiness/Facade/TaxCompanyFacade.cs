
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TaxCompanyFacade : BaseFacade
	{
		protected static TaxCompanyFacade instance = new TaxCompanyFacade(new TaxCompanyModel());
		protected TaxCompanyFacade(TaxCompanyModel model) : base(model)
		{
		}
		public static TaxCompanyFacade Instance
		{
			get { return instance; }
		}
		protected TaxCompanyFacade():base() 
		{ 
		} 
	
	}
}
	