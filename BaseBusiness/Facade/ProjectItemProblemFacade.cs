
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectItemProblemFacade : BaseFacade
	{
		protected static ProjectItemProblemFacade instance = new ProjectItemProblemFacade(new ProjectItemProblemModel());
		protected ProjectItemProblemFacade(ProjectItemProblemModel model) : base(model)
		{
		}
		public static ProjectItemProblemFacade Instance
		{
			get { return instance; }
		}
		protected ProjectItemProblemFacade():base() 
		{ 
		} 
	
	}
}
	