
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectHistoryProblemDetailFacade : BaseFacade
	{
		protected static ProjectHistoryProblemDetailFacade instance = new ProjectHistoryProblemDetailFacade(new ProjectHistoryProblemDetailModel());
		protected ProjectHistoryProblemDetailFacade(ProjectHistoryProblemDetailModel model) : base(model)
		{
		}
		public static ProjectHistoryProblemDetailFacade Instance
		{
			get { return instance; }
		}
		protected ProjectHistoryProblemDetailFacade():base() 
		{ 
		} 
	
	}
}
	