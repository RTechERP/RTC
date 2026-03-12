
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ViewDetailKPIPurchaseFacade : BaseFacade
	{
		protected static ViewDetailKPIPurchaseFacade instance = new ViewDetailKPIPurchaseFacade(new ViewDetailKPIPurchaseModel());
		protected ViewDetailKPIPurchaseFacade(ViewDetailKPIPurchaseModel model) : base(model)
		{
		}
		public static ViewDetailKPIPurchaseFacade Instance
		{
			get { return instance; }
		}
		protected ViewDetailKPIPurchaseFacade():base() 
		{ 
		} 
	
	}
}
	