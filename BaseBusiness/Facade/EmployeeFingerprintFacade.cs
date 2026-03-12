
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeFingerprintFacade : BaseFacade
	{
		protected static EmployeeFingerprintFacade instance = new EmployeeFingerprintFacade(new EmployeeFingerprintModel());
		protected EmployeeFingerprintFacade(EmployeeFingerprintModel model) : base(model)
		{
		}
		public static EmployeeFingerprintFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeFingerprintFacade():base() 
		{ 
		} 
	
	}
}
	