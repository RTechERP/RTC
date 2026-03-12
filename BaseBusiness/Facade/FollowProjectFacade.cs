
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class FollowProjectFacade : BaseFacade
	{
		protected static FollowProjectFacade instance = new FollowProjectFacade(new FollowProjectModel());
		protected FollowProjectFacade(FollowProjectModel model) : base(model)
		{
		}
		public static FollowProjectFacade Instance
		{
			get { return instance; }
		}
		protected FollowProjectFacade():base() 
		{ 
		} 
	
	}
}
	