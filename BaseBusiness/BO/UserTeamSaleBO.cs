
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class UserTeamSaleBO : BaseBO
	{
		private UserTeamSaleFacade facade = UserTeamSaleFacade.Instance;
		protected static UserTeamSaleBO instance = new UserTeamSaleBO();

		protected UserTeamSaleBO()
		{
			this.baseFacade = facade;
		}

		public static UserTeamSaleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	