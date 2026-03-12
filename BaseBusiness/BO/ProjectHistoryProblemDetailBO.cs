
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectHistoryProblemDetailBO : BaseBO
	{
		private ProjectHistoryProblemDetailFacade facade = ProjectHistoryProblemDetailFacade.Instance;
		protected static ProjectHistoryProblemDetailBO instance = new ProjectHistoryProblemDetailBO();

		protected ProjectHistoryProblemDetailBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectHistoryProblemDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	