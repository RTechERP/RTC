
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeTinhTrangHonNhanBO : BaseBO
	{
		private EmployeeTinhTrangHonNhanFacade facade = EmployeeTinhTrangHonNhanFacade.Instance;
		protected static EmployeeTinhTrangHonNhanBO instance = new EmployeeTinhTrangHonNhanBO();

		protected EmployeeTinhTrangHonNhanBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeTinhTrangHonNhanBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	