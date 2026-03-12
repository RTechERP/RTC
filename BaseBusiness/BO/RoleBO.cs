
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RoleBO : BaseBO
	{
		private RoleFacade facade = RoleFacade.Instance;
		protected static RoleBO instance = new RoleBO();

		protected RoleBO()
		{
			this.baseFacade = facade;
		}

		public static RoleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	