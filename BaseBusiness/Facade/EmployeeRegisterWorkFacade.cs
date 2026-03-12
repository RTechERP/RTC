
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeRegisterWorkFacade : BaseFacade
	{
		protected static EmployeeRegisterWorkFacade instance = new EmployeeRegisterWorkFacade(new EmployeeRegisterWorkModel());
		protected EmployeeRegisterWorkFacade(EmployeeRegisterWorkModel model) : base(model)
		{
		}
		public static EmployeeRegisterWorkFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeRegisterWorkFacade():base() 
		{ 
		} 
	
	}
}
	