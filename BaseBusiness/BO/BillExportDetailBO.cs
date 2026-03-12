
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillExportDetailBO : BaseBO
	{
		private BillExportDetailFacade facade = BillExportDetailFacade.Instance;
		protected static BillExportDetailBO instance = new BillExportDetailBO();

		protected BillExportDetailBO()
		{
			this.baseFacade = facade;
		}

		public static BillExportDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	