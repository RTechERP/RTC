
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectItemDetailFacade : BaseFacade
	{
		protected static ProjectItemDetailFacade instance = new ProjectItemDetailFacade(new ProjectItemDetailModel());
		protected ProjectItemDetailFacade(ProjectItemDetailModel model) : base(model)
		{
		}
		public static ProjectItemDetailFacade Instance
		{
			get { return instance; }
		}
		protected ProjectItemDetailFacade():base() 
		{ 
		} 
	
	}
}
	