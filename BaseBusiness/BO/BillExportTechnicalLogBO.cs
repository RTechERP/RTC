
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillExportTechnicalLogBO : BaseBO
	{
		private BillExportTechnicalLogFacade facade = BillExportTechnicalLogFacade.Instance;
		protected static BillExportTechnicalLogBO instance = new BillExportTechnicalLogBO();

		protected BillExportTechnicalLogBO()
		{
			this.baseFacade = facade;
		}

		public static BillExportTechnicalLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	