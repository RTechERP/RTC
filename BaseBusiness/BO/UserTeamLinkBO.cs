
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class UserTeamLinkBO : BaseBO
	{
		private UserTeamLinkFacade facade = UserTeamLinkFacade.Instance;
		protected static UserTeamLinkBO instance = new UserTeamLinkBO();

		protected UserTeamLinkBO()
		{
			this.baseFacade = facade;
		}

		public static UserTeamLinkBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	