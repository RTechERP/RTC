
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeChucVuBO : BaseBO
	{
		private EmployeeChucVuFacade facade = EmployeeChucVuFacade.Instance;
		protected static EmployeeChucVuBO instance = new EmployeeChucVuBO();

		protected EmployeeChucVuBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeChucVuBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	