
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectTypeLinkFacade : BaseFacade
	{
		protected static ProjectTypeLinkFacade instance = new ProjectTypeLinkFacade(new ProjectTypeLinkModel());
		protected ProjectTypeLinkFacade(ProjectTypeLinkModel model) : base(model)
		{
		}
		public static ProjectTypeLinkFacade Instance
		{
			get { return instance; }
		}
		protected ProjectTypeLinkFacade():base() 
		{ 
		} 
	
	}
}
	