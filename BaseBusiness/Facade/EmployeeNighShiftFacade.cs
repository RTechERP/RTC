
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeNighShiftFacade : BaseFacade
	{
		protected static EmployeeNighShiftFacade instance = new EmployeeNighShiftFacade(new EmployeeNighShiftModel());
		protected EmployeeNighShiftFacade(EmployeeNighShiftModel model) : base(model)
		{
		}
		public static EmployeeNighShiftFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeNighShiftFacade():base() 
		{ 
		} 
	
	}
}
	