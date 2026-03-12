
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectItemFacade : BaseFacade
	{
		protected static ProjectItemFacade instance = new ProjectItemFacade(new ProjectItemModel());
		protected ProjectItemFacade(ProjectItemModel model) : base(model)
		{
		}
		public static ProjectItemFacade Instance
		{
			get { return instance; }
		}
		protected ProjectItemFacade():base() 
		{ 
		} 
	
	}
}
	