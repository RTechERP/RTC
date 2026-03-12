
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RoleFacade : BaseFacade
	{
		protected static RoleFacade instance = new RoleFacade(new RoleModel());
		protected RoleFacade(RoleModel model) : base(model)
		{
		}
		public static RoleFacade Instance
		{
			get { return instance; }
		}
		protected RoleFacade():base() 
		{ 
		} 
	
	}
}
	