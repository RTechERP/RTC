
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TeamBO : BaseBO
	{
		private TeamFacade facade = TeamFacade.Instance;
		protected static TeamBO instance = new TeamBO();

		protected TeamBO()
		{
			this.baseFacade = facade;
		}

		public static TeamBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	