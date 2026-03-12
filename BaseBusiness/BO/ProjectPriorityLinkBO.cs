
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectPriorityLinkBO : BaseBO
	{
		private ProjectPriorityLinkFacade facade = ProjectPriorityLinkFacade.Instance;
		protected static ProjectPriorityLinkBO instance = new ProjectPriorityLinkBO();

		protected ProjectPriorityLinkBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectPriorityLinkBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	