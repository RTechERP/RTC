
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class SaleUserTypeFacade : BaseFacade
	{
		protected static SaleUserTypeFacade instance = new SaleUserTypeFacade(new SaleUserTypeModel());
		protected SaleUserTypeFacade(SaleUserTypeModel model) : base(model)
		{
		}
		public static SaleUserTypeFacade Instance
		{
			get { return instance; }
		}
		protected SaleUserTypeFacade():base() 
		{ 
		} 
	
	}
}
	