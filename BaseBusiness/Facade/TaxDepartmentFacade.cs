
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TaxDepartmentFacade : BaseFacade
	{
		protected static TaxDepartmentFacade instance = new TaxDepartmentFacade(new TaxDepartmentModel());
		protected TaxDepartmentFacade(TaxDepartmentModel model) : base(model)
		{
		}
		public static TaxDepartmentFacade Instance
		{
			get { return instance; }
		}
		protected TaxDepartmentFacade():base() 
		{ 
		} 
	
	}
}
	