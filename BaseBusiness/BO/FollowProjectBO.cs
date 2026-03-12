
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class FollowProjectBO : BaseBO
	{
		private FollowProjectFacade facade = FollowProjectFacade.Instance;
		protected static FollowProjectBO instance = new FollowProjectBO();

		protected FollowProjectBO()
		{
			this.baseFacade = facade;
		}

		public static FollowProjectBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	