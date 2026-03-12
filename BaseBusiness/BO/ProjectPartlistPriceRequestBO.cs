
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectPartlistPriceRequestBO : BaseBO
	{
		private ProjectPartlistPriceRequestFacade facade = ProjectPartlistPriceRequestFacade.Instance;
		protected static ProjectPartlistPriceRequestBO instance = new ProjectPartlistPriceRequestBO();

		protected ProjectPartlistPriceRequestBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectPartlistPriceRequestBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	