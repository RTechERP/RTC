
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RegisterIdeaDetailFacade : BaseFacade
	{
		protected static RegisterIdeaDetailFacade instance = new RegisterIdeaDetailFacade(new RegisterIdeaDetailModel());
		protected RegisterIdeaDetailFacade(RegisterIdeaDetailModel model) : base(model)
		{
		}
		public static RegisterIdeaDetailFacade Instance
		{
			get { return instance; }
		}
		protected RegisterIdeaDetailFacade():base() 
		{ 
		} 
	
	}
}
	