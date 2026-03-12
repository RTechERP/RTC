
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectPartListVersionFacade : BaseFacade
	{
		protected static ProjectPartListVersionFacade instance = new ProjectPartListVersionFacade(new ProjectPartListVersionModel());
		protected ProjectPartListVersionFacade(ProjectPartListVersionModel model) : base(model)
		{
		}
		public static ProjectPartListVersionFacade Instance
		{
			get { return instance; }
		}
		protected ProjectPartListVersionFacade():base() 
		{ 
		} 
	
	}
}
	