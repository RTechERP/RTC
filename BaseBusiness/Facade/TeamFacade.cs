
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TeamFacade : BaseFacade
	{
		protected static TeamFacade instance = new TeamFacade(new TeamModel());
		protected TeamFacade(TeamModel model) : base(model)
		{
		}
		public static TeamFacade Instance
		{
			get { return instance; }
		}
		protected TeamFacade():base() 
		{ 
		} 
	
	}
}
	