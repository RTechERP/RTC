
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillExportDetailTechnicalBO : BaseBO
	{
		private BillExportDetailTechnicalFacade facade = BillExportDetailTechnicalFacade.Instance;
		protected static BillExportDetailTechnicalBO instance = new BillExportDetailTechnicalBO();

		protected BillExportDetailTechnicalBO()
		{
			this.baseFacade = facade;
		}

		public static BillExportDetailTechnicalBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	