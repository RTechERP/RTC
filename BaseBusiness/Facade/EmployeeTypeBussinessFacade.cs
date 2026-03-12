
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeTypeBussinessFacade : BaseFacade
	{
		protected static EmployeeTypeBussinessFacade instance = new EmployeeTypeBussinessFacade(new EmployeeTypeBussinessModel());
		protected EmployeeTypeBussinessFacade(EmployeeTypeBussinessModel model) : base(model)
		{
		}
		public static EmployeeTypeBussinessFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeTypeBussinessFacade():base() 
		{ 
		} 
	
	}
}
	