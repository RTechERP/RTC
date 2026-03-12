
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectRequestFacade : BaseFacade
	{
		protected static ProjectRequestFacade instance = new ProjectRequestFacade(new ProjectRequestModel());
		protected ProjectRequestFacade(ProjectRequestModel model) : base(model)
		{
		}
		public static ProjectRequestFacade Instance
		{
			get { return instance; }
		}
		protected ProjectRequestFacade():base() 
		{ 
		} 
	
	}
}
	