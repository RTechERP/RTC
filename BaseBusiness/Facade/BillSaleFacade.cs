
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillSaleFacade : BaseFacade
	{
		protected static BillSaleFacade instance = new BillSaleFacade(new BillSaleModel());
		protected BillSaleFacade(BillSaleModel model) : base(model)
		{
		}
		public static BillSaleFacade Instance
		{
			get { return instance; }
		}
		protected BillSaleFacade():base() 
		{ 
		} 
	
	}
}
	