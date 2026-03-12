
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectTypeFacade : BaseFacade
	{
		protected static ProjectTypeFacade instance = new ProjectTypeFacade(new ProjectTypeModel());
		protected ProjectTypeFacade(ProjectTypeModel model) : base(model)
		{
		}
		public static ProjectTypeFacade Instance
		{
			get { return instance; }
		}
		protected ProjectTypeFacade():base() 
		{ 
		} 
	
	}
}
	