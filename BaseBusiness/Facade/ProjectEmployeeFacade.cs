
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectEmployeeFacade : BaseFacade
	{
		protected static ProjectEmployeeFacade instance = new ProjectEmployeeFacade(new ProjectEmployeeModel());
		protected ProjectEmployeeFacade(ProjectEmployeeModel model) : base(model)
		{
		}
		public static ProjectEmployeeFacade Instance
		{
			get { return instance; }
		}
		protected ProjectEmployeeFacade():base() 
		{ 
		} 
	
	}
}
	