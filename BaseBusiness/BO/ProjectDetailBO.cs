
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectDetailBO : BaseBO
	{
		private ProjectDetailFacade facade = ProjectDetailFacade.Instance;
		protected static ProjectDetailBO instance = new ProjectDetailBO();

		protected ProjectDetailBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	