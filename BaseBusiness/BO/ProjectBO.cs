
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectBO : BaseBO
	{
		private ProjectFacade facade = ProjectFacade.Instance;
		protected static ProjectBO instance = new ProjectBO();

		protected ProjectBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	