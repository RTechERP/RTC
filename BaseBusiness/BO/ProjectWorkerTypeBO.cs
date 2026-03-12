
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectWorkerTypeBO : BaseBO
	{
		private ProjectWorkerTypeFacade facade = ProjectWorkerTypeFacade.Instance;
		protected static ProjectWorkerTypeBO instance = new ProjectWorkerTypeBO();

		protected ProjectWorkerTypeBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectWorkerTypeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	