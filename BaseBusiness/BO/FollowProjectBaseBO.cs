
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class FollowProjectBaseBO : BaseBO
	{
		private FollowProjectBaseFacade facade = FollowProjectBaseFacade.Instance;
		protected static FollowProjectBaseBO instance = new FollowProjectBaseBO();

		protected FollowProjectBaseBO()
		{
			this.baseFacade = facade;
		}

		public static FollowProjectBaseBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	