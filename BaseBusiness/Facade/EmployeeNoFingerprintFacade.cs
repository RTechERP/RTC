
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeNoFingerprintFacade : BaseFacade
	{
		protected static EmployeeNoFingerprintFacade instance = new EmployeeNoFingerprintFacade(new EmployeeNoFingerprintModel());
		protected EmployeeNoFingerprintFacade(EmployeeNoFingerprintModel model) : base(model)
		{
		}
		public static EmployeeNoFingerprintFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeNoFingerprintFacade():base() 
		{ 
		} 
	
	}
}
	