
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProjectPartlistPurchaseRequestFacade : BaseFacade
	{
		protected static ProjectPartlistPurchaseRequestFacade instance = new ProjectPartlistPurchaseRequestFacade(new ProjectPartlistPurchaseRequestModel());
		protected ProjectPartlistPurchaseRequestFacade(ProjectPartlistPurchaseRequestModel model) : base(model)
		{
		}
		public static ProjectPartlistPurchaseRequestFacade Instance
		{
			get { return instance; }
		}
		protected ProjectPartlistPurchaseRequestFacade():base() 
		{ 
		} 
	
	}
}
	