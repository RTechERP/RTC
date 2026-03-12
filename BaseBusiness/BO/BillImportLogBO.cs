
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillImportLogBO : BaseBO
	{
		private BillImportLogFacade facade = BillImportLogFacade.Instance;
		protected static BillImportLogBO instance = new BillImportLogBO();

		protected BillImportLogBO()
		{
			this.baseFacade = facade;
		}

		public static BillImportLogBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	