
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectPartlistPriceRequestFacade : BaseFacade
	{
		protected static ProjectPartlistPriceRequestFacade instance = new ProjectPartlistPriceRequestFacade(new ProjectPartlistPriceRequestModel());
		protected ProjectPartlistPriceRequestFacade(ProjectPartlistPriceRequestModel model) : base(model)
		{
		}
		public static ProjectPartlistPriceRequestFacade Instance
		{
			get { return instance; }
		}
		protected ProjectPartlistPriceRequestFacade():base() 
		{ 
		} 
	
	}
}
	