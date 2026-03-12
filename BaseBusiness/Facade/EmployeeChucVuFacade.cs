
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeChucVuFacade : BaseFacade
	{
		protected static EmployeeChucVuFacade instance = new EmployeeChucVuFacade(new EmployeeChucVuModel());
		protected EmployeeChucVuFacade(EmployeeChucVuModel model) : base(model)
		{
		}
		public static EmployeeChucVuFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeChucVuFacade():base() 
		{ 
		} 
	
	}
}
	