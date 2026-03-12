
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectRequestFileBO : BaseBO
	{
		private ProjectRequestFileFacade facade = ProjectRequestFileFacade.Instance;
		protected static ProjectRequestFileBO instance = new ProjectRequestFileBO();

		protected ProjectRequestFileBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectRequestFileBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	