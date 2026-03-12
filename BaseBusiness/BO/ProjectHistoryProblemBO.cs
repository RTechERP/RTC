
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectHistoryProblemBO : BaseBO
	{
		private ProjectHistoryProblemFacade facade = ProjectHistoryProblemFacade.Instance;
		protected static ProjectHistoryProblemBO instance = new ProjectHistoryProblemBO();

		protected ProjectHistoryProblemBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectHistoryProblemBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	