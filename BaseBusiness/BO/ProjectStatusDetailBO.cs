
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectStatusDetailBO : BaseBO
	{
		private ProjectStatusDetailFacade facade = ProjectStatusDetailFacade.Instance;
		protected static ProjectStatusDetailBO instance = new ProjectStatusDetailBO();

		protected ProjectStatusDetailBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectStatusDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	