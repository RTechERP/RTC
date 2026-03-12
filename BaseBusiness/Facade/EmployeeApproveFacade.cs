
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeApproveFacade : BaseFacade
	{
		protected static EmployeeApproveFacade instance = new EmployeeApproveFacade(new EmployeeApproveModel());
		protected EmployeeApproveFacade(EmployeeApproveModel model) : base(model)
		{
		}
		public static EmployeeApproveFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeApproveFacade():base() 
		{ 
		} 
	
	}
}
	