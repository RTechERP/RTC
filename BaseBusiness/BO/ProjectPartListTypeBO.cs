
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectPartListTypeBO : BaseBO
	{
		private ProjectPartListTypeFacade facade = ProjectPartListTypeFacade.Instance;
		protected static ProjectPartListTypeBO instance = new ProjectPartListTypeBO();

		protected ProjectPartListTypeBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectPartListTypeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	