
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectPartlistPriceRequestHistoryFacade : BaseFacade
	{
		protected static ProjectPartlistPriceRequestHistoryFacade instance = new ProjectPartlistPriceRequestHistoryFacade(new ProjectPartlistPriceRequestHistoryModel());
		protected ProjectPartlistPriceRequestHistoryFacade(ProjectPartlistPriceRequestHistoryModel model) : base(model)
		{
		}
		public static ProjectPartlistPriceRequestHistoryFacade Instance
		{
			get { return instance; }
		}
		protected ProjectPartlistPriceRequestHistoryFacade():base() 
		{ 
		} 
	
	}
}
	