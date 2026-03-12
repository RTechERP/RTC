
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillImportTechnicalBO : BaseBO
	{
		private BillImportTechnicalFacade facade = BillImportTechnicalFacade.Instance;
		protected static BillImportTechnicalBO instance = new BillImportTechnicalBO();

		protected BillImportTechnicalBO()
		{
			this.baseFacade = facade;
		}

		public static BillImportTechnicalBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	