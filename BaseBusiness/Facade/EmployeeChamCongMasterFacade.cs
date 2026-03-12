
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeChamCongMasterFacade : BaseFacade
	{
		protected static EmployeeChamCongMasterFacade instance = new EmployeeChamCongMasterFacade(new EmployeeChamCongMasterModel());
		protected EmployeeChamCongMasterFacade(EmployeeChamCongMasterModel model) : base(model)
		{
		}
		public static EmployeeChamCongMasterFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeChamCongMasterFacade():base() 
		{ 
		} 
	
	}
}
	