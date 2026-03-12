
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillExportTechnicalBO : BaseBO
	{
		private BillExportTechnicalFacade facade = BillExportTechnicalFacade.Instance;
		protected static BillExportTechnicalBO instance = new BillExportTechnicalBO();

		protected BillExportTechnicalBO()
		{
			this.baseFacade = facade;
		}

		public static BillExportTechnicalBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	