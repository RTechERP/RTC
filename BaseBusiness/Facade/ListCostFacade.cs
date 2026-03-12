
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ListCostFacade : BaseFacade
	{
		protected static ListCostFacade instance = new ListCostFacade(new ListCostModel());
		protected ListCostFacade(ListCostModel model) : base(model)
		{
		}
		public static ListCostFacade Instance
		{
			get { return instance; }
		}
		protected ListCostFacade():base() 
		{ 
		} 
	
	}
}
	