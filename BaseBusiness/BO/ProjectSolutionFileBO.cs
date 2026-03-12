
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectSolutionFileBO : BaseBO
	{
		private ProjectSolutionFileFacade facade = ProjectSolutionFileFacade.Instance;
		protected static ProjectSolutionFileBO instance = new ProjectSolutionFileBO();

		protected ProjectSolutionFileBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectSolutionFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	