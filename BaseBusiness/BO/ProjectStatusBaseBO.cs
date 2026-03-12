
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectStatusBaseBO : BaseBO
	{
		private ProjectStatusBaseFacade facade = ProjectStatusBaseFacade.Instance;
		protected static ProjectStatusBaseBO instance = new ProjectStatusBaseBO();

		protected ProjectStatusBaseBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectStatusBaseBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	