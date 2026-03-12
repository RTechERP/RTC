
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ReportPurchaseBO : BaseBO
	{
		private ReportPurchaseFacade facade = ReportPurchaseFacade.Instance;
		protected static ReportPurchaseBO instance = new ReportPurchaseBO();

		protected ReportPurchaseBO()
		{
			this.baseFacade = facade;
		}

		public static ReportPurchaseBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	