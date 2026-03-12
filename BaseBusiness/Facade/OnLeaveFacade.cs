
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class OnLeaveFacade : BaseFacade
	{
		protected static OnLeaveFacade instance = new OnLeaveFacade(new OnLeaveModel());
		protected OnLeaveFacade(OnLeaveModel model) : base(model)
		{
		}
		public static OnLeaveFacade Instance
		{
			get { return instance; }
		}
		protected OnLeaveFacade():base() 
		{ 
		} 
	
	}
}
	