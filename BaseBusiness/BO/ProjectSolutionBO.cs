
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectSolutionBO : BaseBO
	{
		private ProjectSolutionFacade facade = ProjectSolutionFacade.Instance;
		protected static ProjectSolutionBO instance = new ProjectSolutionBO();

		protected ProjectSolutionBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectSolutionBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	