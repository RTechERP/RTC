
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeChamCongDetailFacade : BaseFacade
	{
		protected static EmployeeChamCongDetailFacade instance = new EmployeeChamCongDetailFacade(new EmployeeChamCongDetailModel());
		protected EmployeeChamCongDetailFacade(EmployeeChamCongDetailModel model) : base(model)
		{
		}
		public static EmployeeChamCongDetailFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeChamCongDetailFacade():base() 
		{ 
		} 
	
	}
}
	