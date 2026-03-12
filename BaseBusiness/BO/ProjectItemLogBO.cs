
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectItemLogBO : BaseBO
	{
		private ProjectItemLogFacade facade = ProjectItemLogFacade.Instance;
		protected static ProjectItemLogBO instance = new ProjectItemLogBO();

		protected ProjectItemLogBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectItemLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	