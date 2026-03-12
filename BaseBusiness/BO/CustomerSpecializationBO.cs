
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CustomerSpecializationBO : BaseBO
	{
		private CustomerSpecializationFacade facade = CustomerSpecializationFacade.Instance;
		protected static CustomerSpecializationBO instance = new CustomerSpecializationBO();

		protected CustomerSpecializationBO()
		{
			this.baseFacade = facade;
		}

		public static CustomerSpecializationBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	