
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectPersonalPriotityFacade : BaseFacade
	{
		protected static ProjectPersonalPriotityFacade instance = new ProjectPersonalPriotityFacade(new ProjectPersonalPriotityModel());
		protected ProjectPersonalPriotityFacade(ProjectPersonalPriotityModel model) : base(model)
		{
		}
		public static ProjectPersonalPriotityFacade Instance
		{
			get { return instance; }
		}
		protected ProjectPersonalPriotityFacade():base() 
		{ 
		} 
	
	}
}
	