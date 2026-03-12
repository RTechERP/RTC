
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RegisterIdeaScoreFacade : BaseFacade
	{
		protected static RegisterIdeaScoreFacade instance = new RegisterIdeaScoreFacade(new RegisterIdeaScoreModel());
		protected RegisterIdeaScoreFacade(RegisterIdeaScoreModel model) : base(model)
		{
		}
		public static RegisterIdeaScoreFacade Instance
		{
			get { return instance; }
		}
		protected RegisterIdeaScoreFacade():base() 
		{ 
		} 
	
	}
}
	