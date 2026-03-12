
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ListCostBO : BaseBO
	{
		private ListCostFacade facade = ListCostFacade.Instance;
		protected static ListCostBO instance = new ListCostBO();

		protected ListCostBO()
		{
			this.baseFacade = facade;
		}

		public static ListCostBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	