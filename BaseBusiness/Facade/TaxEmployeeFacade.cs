
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TaxEmployeeFacade : BaseFacade
	{
		protected static TaxEmployeeFacade instance = new TaxEmployeeFacade(new TaxEmployeeModel());
		protected TaxEmployeeFacade(TaxEmployeeModel model) : base(model)
		{
		}
		public static TaxEmployeeFacade Instance
		{
			get { return instance; }
		}
		protected TaxEmployeeFacade():base() 
		{ 
		} 
	
	}
}
	