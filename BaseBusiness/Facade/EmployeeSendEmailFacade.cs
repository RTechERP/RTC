
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeSendEmailFacade : BaseFacade
	{
		protected static EmployeeSendEmailFacade instance = new EmployeeSendEmailFacade(new EmployeeSendEmailModel());
		protected EmployeeSendEmailFacade(EmployeeSendEmailModel model) : base(model)
		{
		}
		public static EmployeeSendEmailFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeSendEmailFacade():base() 
		{ 
		} 
	
	}
}
	