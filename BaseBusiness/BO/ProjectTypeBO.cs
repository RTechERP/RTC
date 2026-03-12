
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectTypeBO : BaseBO
	{
		private ProjectTypeFacade facade = ProjectTypeFacade.Instance;
		protected static ProjectTypeBO instance = new ProjectTypeBO();

		protected ProjectTypeBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectTypeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	