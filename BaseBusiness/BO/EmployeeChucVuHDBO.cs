
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeChucVuHDBO : BaseBO
	{
		private EmployeeChucVuHDFacade facade = EmployeeChucVuHDFacade.Instance;
		protected static EmployeeChucVuHDBO instance = new EmployeeChucVuHDBO();

		protected EmployeeChucVuHDBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeChucVuHDBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	