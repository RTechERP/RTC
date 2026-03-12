
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class UserTeamSaleFacade : BaseFacade
	{
		protected static UserTeamSaleFacade instance = new UserTeamSaleFacade(new UserTeamSaleModel());
		protected UserTeamSaleFacade(UserTeamSaleModel model) : base(model)
		{
		}
		public static UserTeamSaleFacade Instance
		{
			get { return instance; }
		}
		protected UserTeamSaleFacade():base() 
		{ 
		} 
	
	}
}
	