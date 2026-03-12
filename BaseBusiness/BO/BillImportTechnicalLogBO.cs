
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillImportTechnicalLogBO : BaseBO
	{
		private BillImportTechnicalLogFacade facade = BillImportTechnicalLogFacade.Instance;
		protected static BillImportTechnicalLogBO instance = new BillImportTechnicalLogBO();

		protected BillImportTechnicalLogBO()
		{
			this.baseFacade = facade;
		}

		public static BillImportTechnicalLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	