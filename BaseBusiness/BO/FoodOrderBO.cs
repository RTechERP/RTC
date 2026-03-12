
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class FoodOrderBO : BaseBO
	{
		private FoodOrderFacade facade = FoodOrderFacade.Instance;
		protected static FoodOrderBO instance = new FoodOrderBO();

		protected FoodOrderBO()
		{
			this.baseFacade = facade;
		}

		public static FoodOrderBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	