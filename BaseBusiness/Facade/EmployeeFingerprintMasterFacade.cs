
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeFingerprintMasterFacade : BaseFacade
	{
		protected static EmployeeFingerprintMasterFacade instance = new EmployeeFingerprintMasterFacade(new EmployeeFingerprintMasterModel());
		protected EmployeeFingerprintMasterFacade(EmployeeFingerprintMasterModel model) : base(model)
		{
		}
		public static EmployeeFingerprintMasterFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeFingerprintMasterFacade():base() 
		{ 
		} 
	
	}
}
	