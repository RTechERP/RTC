
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class AuditLogFacade : BaseFacade
	{
		protected static AuditLogFacade instance = new AuditLogFacade(new AuditLogModel());
		protected AuditLogFacade(AuditLogModel model) : base(model)
		{
		}
		public static AuditLogFacade Instance
		{
			get { return instance; }
		}
		protected AuditLogFacade():base() 
		{ 
		} 
	
	}
}
	