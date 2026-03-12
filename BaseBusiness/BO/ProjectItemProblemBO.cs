
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectItemProblemBO : BaseBO
	{
		private ProjectItemProblemFacade facade = ProjectItemProblemFacade.Instance;
		protected static ProjectItemProblemBO instance = new ProjectItemProblemBO();

		protected ProjectItemProblemBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectItemProblemBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	