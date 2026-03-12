
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectItemDetailBO : BaseBO
	{
		private ProjectItemDetailFacade facade = ProjectItemDetailFacade.Instance;
		protected static ProjectItemDetailBO instance = new ProjectItemDetailBO();

		protected ProjectItemDetailBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectItemDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	