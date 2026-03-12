
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeChucVuHDFacade : BaseFacade
	{
		protected static EmployeeChucVuHDFacade instance = new EmployeeChucVuHDFacade(new EmployeeChucVuHDModel());
		protected EmployeeChucVuHDFacade(EmployeeChucVuHDModel model) : base(model)
		{
		}
		public static EmployeeChucVuHDFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeChucVuHDFacade():base() 
		{ 
		} 
	
	}
}
	