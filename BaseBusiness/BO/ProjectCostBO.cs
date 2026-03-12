
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectCostBO : BaseBO
	{
		private ProjectCostFacade facade = ProjectCostFacade.Instance;
		protected static ProjectCostBO instance = new ProjectCostBO();

		protected ProjectCostBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectCostBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	