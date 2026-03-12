
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class AuditLogBO : BaseBO
	{
		private AuditLogFacade facade = AuditLogFacade.Instance;
		protected static AuditLogBO instance = new AuditLogBO();

		protected AuditLogBO()
		{
			this.baseFacade = facade;
		}

		public static AuditLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	