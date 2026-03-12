
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectPartlistPriceRequestHistoryBO : BaseBO
	{
		private ProjectPartlistPriceRequestHistoryFacade facade = ProjectPartlistPriceRequestHistoryFacade.Instance;
		protected static ProjectPartlistPriceRequestHistoryBO instance = new ProjectPartlistPriceRequestHistoryBO();

		protected ProjectPartlistPriceRequestHistoryBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectPartlistPriceRequestHistoryBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	