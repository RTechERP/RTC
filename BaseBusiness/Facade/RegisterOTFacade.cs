
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RegisterOTFacade : BaseFacade
	{
		protected static RegisterOTFacade instance = new RegisterOTFacade(new RegisterOTModel());
		protected RegisterOTFacade(RegisterOTModel model) : base(model)
		{
		}
		public static RegisterOTFacade Instance
		{
			get { return instance; }
		}
		protected RegisterOTFacade():base() 
		{ 
		} 
	
	}
}
	