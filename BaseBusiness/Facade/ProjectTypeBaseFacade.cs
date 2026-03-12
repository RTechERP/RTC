
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectTypeBaseFacade : BaseFacade
	{
		protected static ProjectTypeBaseFacade instance = new ProjectTypeBaseFacade(new ProjectTypeBaseModel());
		protected ProjectTypeBaseFacade(ProjectTypeBaseModel model) : base(model)
		{
		}
		public static ProjectTypeBaseFacade Instance
		{
			get { return instance; }
		}
		protected ProjectTypeBaseFacade():base() 
		{ 
		} 
	
	}
}
	