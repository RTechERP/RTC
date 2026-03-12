
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillImportDetailSerialNumberBO : BaseBO
	{
		private BillImportDetailSerialNumberFacade facade = BillImportDetailSerialNumberFacade.Instance;
		protected static BillImportDetailSerialNumberBO instance = new BillImportDetailSerialNumberBO();

		protected BillImportDetailSerialNumberBO()
		{
			this.baseFacade = facade;
		}

		public static BillImportDetailSerialNumberBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	