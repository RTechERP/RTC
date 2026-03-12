
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeLoaiHDLDFacade : BaseFacade
	{
		protected static EmployeeLoaiHDLDFacade instance = new EmployeeLoaiHDLDFacade(new EmployeeLoaiHDLDModel());
		protected EmployeeLoaiHDLDFacade(EmployeeLoaiHDLDModel model) : base(model)
		{
		}
		public static EmployeeLoaiHDLDFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeLoaiHDLDFacade():base() 
		{ 
		} 
	
	}
}
	