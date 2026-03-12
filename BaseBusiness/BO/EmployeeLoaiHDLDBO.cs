
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeLoaiHDLDBO : BaseBO
	{
		private EmployeeLoaiHDLDFacade facade = EmployeeLoaiHDLDFacade.Instance;
		protected static EmployeeLoaiHDLDBO instance = new EmployeeLoaiHDLDBO();

		protected EmployeeLoaiHDLDBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeLoaiHDLDBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	