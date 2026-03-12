
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProjectPartlistPurchaseRequestBO : BaseBO
	{
		private ProjectPartlistPurchaseRequestFacade facade = ProjectPartlistPurchaseRequestFacade.Instance;
		protected static ProjectPartlistPurchaseRequestBO instance = new ProjectPartlistPurchaseRequestBO();

		protected ProjectPartlistPurchaseRequestBO()
		{
			this.baseFacade = facade;
		}

		public static ProjectPartlistPurchaseRequestBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	