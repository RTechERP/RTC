
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CustomerBaseContactBO : BaseBO
	{
		private CustomerBaseContactFacade facade = CustomerBaseContactFacade.Instance;
		protected static CustomerBaseContactBO instance = new CustomerBaseContactBO();

		protected CustomerBaseContactBO()
		{
			this.baseFacade = facade;
		}

		public static CustomerBaseContactBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	