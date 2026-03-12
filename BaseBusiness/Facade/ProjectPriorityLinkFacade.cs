
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectPriorityLinkFacade : BaseFacade
	{
		protected static ProjectPriorityLinkFacade instance = new ProjectPriorityLinkFacade(new ProjectPriorityLinkModel());
		protected ProjectPriorityLinkFacade(ProjectPriorityLinkModel model) : base(model)
		{
		}
		public static ProjectPriorityLinkFacade Instance
		{
			get { return instance; }
		}
		protected ProjectPriorityLinkFacade():base() 
		{ 
		} 
	
	}
}
	