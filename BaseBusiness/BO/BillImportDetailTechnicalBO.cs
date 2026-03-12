
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillImportDetailTechnicalBO : BaseBO
	{
		private BillImportDetailTechnicalFacade facade = BillImportDetailTechnicalFacade.Instance;
		protected static BillImportDetailTechnicalBO instance = new BillImportDetailTechnicalBO();

		protected BillImportDetailTechnicalBO()
		{
			this.baseFacade = facade;
		}

		public static BillImportDetailTechnicalBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	