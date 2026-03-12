
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectTypeBaseBO : BaseBO
	{
		private ProjectTypeBaseFacade facade = ProjectTypeBaseFacade.Instance;
		protected static ProjectTypeBaseBO instance = new ProjectTypeBaseBO();

		protected ProjectTypeBaseBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectTypeBaseBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	