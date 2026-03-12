
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class UserTeamBO : BaseBO
	{
		private UserTeamFacade facade = UserTeamFacade.Instance;
		protected static UserTeamBO instance = new UserTeamBO();

		protected UserTeamBO()
		{
			this.baseFacade = facade;
		}

		public static UserTeamBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	