
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class FollowProjectBaseFacade : BaseFacade
	{
		protected static FollowProjectBaseFacade instance = new FollowProjectBaseFacade(new FollowProjectBaseModel());
		protected FollowProjectBaseFacade(FollowProjectBaseModel model) : base(model)
		{
		}
		public static FollowProjectBaseFacade Instance
		{
			get { return instance; }
		}
		protected FollowProjectBaseFacade():base() 
		{ 
		} 
	
	}
}
	