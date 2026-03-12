
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeBussinessFacade : BaseFacade
	{
		protected static EmployeeBussinessFacade instance = new EmployeeBussinessFacade(new EmployeeBussinessModel());
		protected EmployeeBussinessFacade(EmployeeBussinessModel model) : base(model)
		{
		}
		public static EmployeeBussinessFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeBussinessFacade():base() 
		{ 
		} 
	
	}
}
	