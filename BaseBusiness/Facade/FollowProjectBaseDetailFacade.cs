
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class FollowProjectBaseDetailFacade : BaseFacade
	{
		protected static FollowProjectBaseDetailFacade instance = new FollowProjectBaseDetailFacade(new FollowProjectBaseDetailModel());
		protected FollowProjectBaseDetailFacade(FollowProjectBaseDetailModel model) : base(model)
		{
		}
		public static FollowProjectBaseDetailFacade Instance
		{
			get { return instance; }
		}
		protected FollowProjectBaseDetailFacade():base() 
		{ 
		} 
	
	}
}
	