
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RegisterIdeaFacade : BaseFacade
	{
		protected static RegisterIdeaFacade instance = new RegisterIdeaFacade(new RegisterIdeaModel());
		protected RegisterIdeaFacade(RegisterIdeaModel model) : base(model)
		{
		}
		public static RegisterIdeaFacade Instance
		{
			get { return instance; }
		}
		protected RegisterIdeaFacade():base() 
		{ 
		} 
	
	}
}
	