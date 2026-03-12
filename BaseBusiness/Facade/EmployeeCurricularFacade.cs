
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeCurricularFacade : BaseFacade
	{
		protected static EmployeeCurricularFacade instance = new EmployeeCurricularFacade(new EmployeeCurricularModel());
		protected EmployeeCurricularFacade(EmployeeCurricularModel model) : base(model)
		{
		}
		public static EmployeeCurricularFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeCurricularFacade():base() 
		{ 
		} 
	
	}
}
	