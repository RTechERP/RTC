
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ReportPurchaseFacade : BaseFacade
	{
		protected static ReportPurchaseFacade instance = new ReportPurchaseFacade(new ReportPurchaseModel());
		protected ReportPurchaseFacade(ReportPurchaseModel model) : base(model)
		{
		}
		public static ReportPurchaseFacade Instance
		{
			get { return instance; }
		}
		protected ReportPurchaseFacade():base() 
		{ 
		} 
	
	}
}
	