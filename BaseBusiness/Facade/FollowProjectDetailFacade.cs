
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class FollowProjectDetailFacade : BaseFacade
	{
		protected static FollowProjectDetailFacade instance = new FollowProjectDetailFacade(new FollowProjectDetailModel());
		protected FollowProjectDetailFacade(FollowProjectDetailModel model) : base(model)
		{
		}
		public static FollowProjectDetailFacade Instance
		{
			get { return instance; }
		}
		protected FollowProjectDetailFacade():base() 
		{ 
		} 
	
	}
}
	