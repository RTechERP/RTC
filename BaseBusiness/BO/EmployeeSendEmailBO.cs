
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeSendEmailBO : BaseBO
	{
		private EmployeeSendEmailFacade facade = EmployeeSendEmailFacade.Instance;
		protected static EmployeeSendEmailBO instance = new EmployeeSendEmailBO();

		protected EmployeeSendEmailBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeSendEmailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	