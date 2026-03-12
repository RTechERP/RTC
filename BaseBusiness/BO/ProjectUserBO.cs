
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectUserBO : BaseBO
	{
		private ProjectUserFacade facade = ProjectUserFacade.Instance;
		protected static ProjectUserBO instance = new ProjectUserBO();

		protected ProjectUserBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectUserBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	