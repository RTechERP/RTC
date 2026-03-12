
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class DiscountOrderBO : BaseBO
	{
		private DiscountOrderFacade facade = DiscountOrderFacade.Instance;
		protected static DiscountOrderBO instance = new DiscountOrderBO();

		protected DiscountOrderBO()
		{
			this.baseFacade = facade;
		}

		public static DiscountOrderBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	