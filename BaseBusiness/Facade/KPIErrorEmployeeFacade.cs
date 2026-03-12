
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class KPIErrorEmployeeFacade : BaseFacade
	{
		protected static KPIErrorEmployeeFacade instance = new KPIErrorEmployeeFacade(new KPIErrorEmployeeModel());
		protected KPIErrorEmployeeFacade(KPIErrorEmployeeModel model) : base(model)
		{
		}
		public static KPIErrorEmployeeFacade Instance
		{
			get { return instance; }
		}
		protected KPIErrorEmployeeFacade():base() 
		{ 
		} 
	
	}
}
	