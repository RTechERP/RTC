
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TaxEmployeePositionFacade : BaseFacade
	{
		protected static TaxEmployeePositionFacade instance = new TaxEmployeePositionFacade(new TaxEmployeePositionModel());
		protected TaxEmployeePositionFacade(TaxEmployeePositionModel model) : base(model)
		{
		}
		public static TaxEmployeePositionFacade Instance
		{
			get { return instance; }
		}
		protected TaxEmployeePositionFacade():base() 
		{ 
		} 
	
	}
}
	