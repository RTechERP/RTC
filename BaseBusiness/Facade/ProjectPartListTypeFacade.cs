
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectPartListTypeFacade : BaseFacade
	{
		protected static ProjectPartListTypeFacade instance = new ProjectPartListTypeFacade(new ProjectPartListTypeModel());
		protected ProjectPartListTypeFacade(ProjectPartListTypeModel model) : base(model)
		{
		}
		public static ProjectPartListTypeFacade Instance
		{
			get { return instance; }
		}
		protected ProjectPartListTypeFacade():base() 
		{ 
		} 
	
	}
}
	