
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeePayrollBonusDeuctionFacade : BaseFacade
	{
		protected static EmployeePayrollBonusDeuctionFacade instance = new EmployeePayrollBonusDeuctionFacade(new EmployeePayrollBonusDeuctionModel());
		protected EmployeePayrollBonusDeuctionFacade(EmployeePayrollBonusDeuctionModel model) : base(model)
		{
		}
		public static EmployeePayrollBonusDeuctionFacade Instance
		{
			get { return instance; }
		}
		protected EmployeePayrollBonusDeuctionFacade():base() 
		{ 
		} 
	
	}
}
	