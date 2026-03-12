
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectPartListFacade : BaseFacade
	{
		protected static ProjectPartListFacade instance = new ProjectPartListFacade(new ProjectPartListModel());
		protected ProjectPartListFacade(ProjectPartListModel model) : base(model)
		{
		}
		public static ProjectPartListFacade Instance
		{
			get { return instance; }
		}
		protected ProjectPartListFacade():base() 
		{ 
		} 
	
	}
}
	