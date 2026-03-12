
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectDetailFacade : BaseFacade
	{
		protected static ProjectDetailFacade instance = new ProjectDetailFacade(new ProjectDetailModel());
		protected ProjectDetailFacade(ProjectDetailModel model) : base(model)
		{
		}
		public static ProjectDetailFacade Instance
		{
			get { return instance; }
		}
		protected ProjectDetailFacade():base() 
		{ 
		} 
	
	}
}
	