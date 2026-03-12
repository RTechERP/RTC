
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectUserFacade : BaseFacade
	{
		protected static ProjectUserFacade instance = new ProjectUserFacade(new ProjectUserModel());
		protected ProjectUserFacade(ProjectUserModel model) : base(model)
		{
		}
		public static ProjectUserFacade Instance
		{
			get { return instance; }
		}
		protected ProjectUserFacade():base() 
		{ 
		} 
	
	}
}
	