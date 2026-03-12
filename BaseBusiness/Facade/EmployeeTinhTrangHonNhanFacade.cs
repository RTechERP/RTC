
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeTinhTrangHonNhanFacade : BaseFacade
	{
		protected static EmployeeTinhTrangHonNhanFacade instance = new EmployeeTinhTrangHonNhanFacade(new EmployeeTinhTrangHonNhanModel());
		protected EmployeeTinhTrangHonNhanFacade(EmployeeTinhTrangHonNhanModel model) : base(model)
		{
		}
		public static EmployeeTinhTrangHonNhanFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeTinhTrangHonNhanFacade():base() 
		{ 
		} 
	
	}
}
	