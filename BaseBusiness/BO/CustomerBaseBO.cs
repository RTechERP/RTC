
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CustomerBaseBO : BaseBO
	{
		private CustomerBaseFacade facade = CustomerBaseFacade.Instance;
		protected static CustomerBaseBO instance = new CustomerBaseBO();

		protected CustomerBaseBO()
		{
			this.baseFacade = facade;
		}

		public static CustomerBaseBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	