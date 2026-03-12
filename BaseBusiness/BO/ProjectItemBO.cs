
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectItemBO : BaseBO
	{
		private ProjectItemFacade facade = ProjectItemFacade.Instance;
		protected static ProjectItemBO instance = new ProjectItemBO();

		protected ProjectItemBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectItemBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	