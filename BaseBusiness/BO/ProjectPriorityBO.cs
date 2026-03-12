
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectPriorityBO : BaseBO
	{
		private ProjectPriorityFacade facade = ProjectPriorityFacade.Instance;
		protected static ProjectPriorityBO instance = new ProjectPriorityBO();

		protected ProjectPriorityBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectPriorityBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	