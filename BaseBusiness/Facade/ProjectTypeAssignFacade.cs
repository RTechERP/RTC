
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectTypeAssignFacade : BaseFacade
	{
		protected static ProjectTypeAssignFacade instance = new ProjectTypeAssignFacade(new ProjectTypeAssignModel());
		protected ProjectTypeAssignFacade(ProjectTypeAssignModel model) : base(model)
		{
		}
		public static ProjectTypeAssignFacade Instance
		{
			get { return instance; }
		}
		protected ProjectTypeAssignFacade():base() 
		{ 
		} 
	
	}
}
	