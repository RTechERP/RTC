
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeCollectMoneyFacade : BaseFacade
	{
		protected static EmployeeCollectMoneyFacade instance = new EmployeeCollectMoneyFacade(new EmployeeCollectMoneyModel());
		protected EmployeeCollectMoneyFacade(EmployeeCollectMoneyModel model) : base(model)
		{
		}
		public static EmployeeCollectMoneyFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeCollectMoneyFacade():base() 
		{ 
		} 
	
	}
}
	