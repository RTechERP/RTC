
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class FollowProjectBaseDetailBO : BaseBO
	{
		private FollowProjectBaseDetailFacade facade = FollowProjectBaseDetailFacade.Instance;
		protected static FollowProjectBaseDetailBO instance = new FollowProjectBaseDetailBO();

		protected FollowProjectBaseDetailBO()
		{
			this.baseFacade = facade;
		}

		public static FollowProjectBaseDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	