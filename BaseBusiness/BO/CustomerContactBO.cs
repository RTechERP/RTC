
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CustomerContactBO : BaseBO
	{
		private CustomerContactFacade facade = CustomerContactFacade.Instance;
		protected static CustomerContactBO instance = new CustomerContactBO();

		protected CustomerContactBO()
		{
			this.baseFacade = facade;
		}

		public static CustomerContactBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	