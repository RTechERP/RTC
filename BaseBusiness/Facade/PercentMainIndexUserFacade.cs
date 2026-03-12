
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PercentMainIndexUserFacade : BaseFacade
	{
		protected static PercentMainIndexUserFacade instance = new PercentMainIndexUserFacade(new PercentMainIndexUserModel());
		protected PercentMainIndexUserFacade(PercentMainIndexUserModel model) : base(model)
		{
		}
		public static PercentMainIndexUserFacade Instance
		{
			get { return instance; }
		}
		protected PercentMainIndexUserFacade():base() 
		{ 
		} 
	
	}
}
	