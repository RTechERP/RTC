
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectTypeLinkBO : BaseBO
	{
		private ProjectTypeLinkFacade facade = ProjectTypeLinkFacade.Instance;
		protected static ProjectTypeLinkBO instance = new ProjectTypeLinkBO();

		protected ProjectTypeLinkBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectTypeLinkBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	