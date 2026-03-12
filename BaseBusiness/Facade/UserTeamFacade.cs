
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class UserTeamFacade : BaseFacade
	{
		protected static UserTeamFacade instance = new UserTeamFacade(new UserTeamModel());
		protected UserTeamFacade(UserTeamModel model) : base(model)
		{
		}
		public static UserTeamFacade Instance
		{
			get { return instance; }
		}
		protected UserTeamFacade():base() 
		{ 
		} 
	
	}
}
	