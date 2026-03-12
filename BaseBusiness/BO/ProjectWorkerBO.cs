
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectWorkerBO : BaseBO
	{
		private ProjectWorkerFacade facade = ProjectWorkerFacade.Instance;
		protected static ProjectWorkerBO instance = new ProjectWorkerBO();

		protected ProjectWorkerBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectWorkerBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	