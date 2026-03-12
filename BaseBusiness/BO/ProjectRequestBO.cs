
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectRequestBO : BaseBO
	{
		private ProjectRequestFacade facade = ProjectRequestFacade.Instance;
		protected static ProjectRequestBO instance = new ProjectRequestBO();

		protected ProjectRequestBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectRequestBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	