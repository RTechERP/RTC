
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillExportDetailSerialNumberBO : BaseBO
	{
		private BillExportDetailSerialNumberFacade facade = BillExportDetailSerialNumberFacade.Instance;
		protected static BillExportDetailSerialNumberBO instance = new BillExportDetailSerialNumberBO();

		protected BillExportDetailSerialNumberBO()
		{
			this.baseFacade = facade;
		}

		public static BillExportDetailSerialNumberBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	