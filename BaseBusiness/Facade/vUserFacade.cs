
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class vUserFacade : BaseFacade
	{
		protected static vUserFacade instance = new vUserFacade(new vUserModel());
		protected vUserFacade(vUserModel model) : base(model)
		{
		}
		public static vUserFacade Instance
		{
			get { return instance; }
		}
		protected vUserFacade():base() 
		{ 
		} 
	
	}
}
	