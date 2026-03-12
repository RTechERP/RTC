
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectWorkerVersionBO : BaseBO
	{
		private ProjectWorkerVersionFacade facade = ProjectWorkerVersionFacade.Instance;
		protected static ProjectWorkerVersionBO instance = new ProjectWorkerVersionBO();

		protected ProjectWorkerVersionBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectWorkerVersionBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	