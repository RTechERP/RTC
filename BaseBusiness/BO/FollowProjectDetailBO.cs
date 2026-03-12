
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class FollowProjectDetailBO : BaseBO
	{
		private FollowProjectDetailFacade facade = FollowProjectDetailFacade.Instance;
		protected static FollowProjectDetailBO instance = new FollowProjectDetailBO();

		protected FollowProjectDetailBO()
		{
			this.baseFacade = facade;
		}

		public static FollowProjectDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	