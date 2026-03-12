
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectEmployeeBO : BaseBO
	{
		private ProjectEmployeeFacade facade = ProjectEmployeeFacade.Instance;
		protected static ProjectEmployeeBO instance = new ProjectEmployeeBO();

		protected ProjectEmployeeBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectEmployeeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	