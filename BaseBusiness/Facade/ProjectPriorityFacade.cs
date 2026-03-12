
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectPriorityFacade : BaseFacade
	{
		protected static ProjectPriorityFacade instance = new ProjectPriorityFacade(new ProjectPriorityModel());
		protected ProjectPriorityFacade(ProjectPriorityModel model) : base(model)
		{
		}
		public static ProjectPriorityFacade Instance
		{
			get { return instance; }
		}
		protected ProjectPriorityFacade():base() 
		{ 
		} 
	
	}
}
	