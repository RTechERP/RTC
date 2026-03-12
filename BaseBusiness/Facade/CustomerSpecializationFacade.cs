
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CustomerSpecializationFacade : BaseFacade
	{
		protected static CustomerSpecializationFacade instance = new CustomerSpecializationFacade(new CustomerSpecializationModel());
		protected CustomerSpecializationFacade(CustomerSpecializationModel model) : base(model)
		{
		}
		public static CustomerSpecializationFacade Instance
		{
			get { return instance; }
		}
		protected CustomerSpecializationFacade():base() 
		{ 
		} 
	
	}
}
	