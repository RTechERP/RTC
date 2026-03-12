
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class SaleUserTypeBO : BaseBO
	{
		private SaleUserTypeFacade facade = SaleUserTypeFacade.Instance;
		protected static SaleUserTypeBO instance = new SaleUserTypeBO();

		protected SaleUserTypeBO()
		{
			this.baseFacade = facade;
		}

		public static SaleUserTypeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	