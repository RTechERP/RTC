
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectRequestFileFacade : BaseFacade
	{
		protected static ProjectRequestFileFacade instance = new ProjectRequestFileFacade(new ProjectRequestFileModel());
		protected ProjectRequestFileFacade(ProjectRequestFileModel model) : base(model)
		{
		}
		public static ProjectRequestFileFacade Instance
		{
			get { return instance; }
		}
		protected ProjectRequestFileFacade():base() 
		{ 
		} 
	
	}
}
	