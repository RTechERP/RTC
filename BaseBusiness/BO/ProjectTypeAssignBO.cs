
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectTypeAssignBO : BaseBO
	{
		private ProjectTypeAssignFacade facade = ProjectTypeAssignFacade.Instance;
		protected static ProjectTypeAssignBO instance = new ProjectTypeAssignBO();

		protected ProjectTypeAssignBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectTypeAssignBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	