
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class UserTeamLinkFacade : BaseFacade
	{
		protected static UserTeamLinkFacade instance = new UserTeamLinkFacade(new UserTeamLinkModel());
		protected UserTeamLinkFacade(UserTeamLinkModel model) : base(model)
		{
		}
		public static UserTeamLinkFacade Instance
		{
			get { return instance; }
		}
		protected UserTeamLinkFacade():base() 
		{ 
		} 
	
	}
}
	