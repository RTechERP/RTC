
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillExportLogBO : BaseBO
	{
		private BillExportLogFacade facade = BillExportLogFacade.Instance;
		protected static BillExportLogBO instance = new BillExportLogBO();

		protected BillExportLogBO()
		{
			this.baseFacade = facade;
		}

		public static BillExportLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	