
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectHistoryProblemFacade : BaseFacade
	{
		protected static ProjectHistoryProblemFacade instance = new ProjectHistoryProblemFacade(new ProjectHistoryProblemModel());
		protected ProjectHistoryProblemFacade(ProjectHistoryProblemModel model) : base(model)
		{
		}
		public static ProjectHistoryProblemFacade Instance
		{
			get { return instance; }
		}
		protected ProjectHistoryProblemFacade():base() 
		{ 
		} 
	
	}
}
	