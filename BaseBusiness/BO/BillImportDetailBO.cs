
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillImportDetailBO : BaseBO
	{
		private BillImportDetailFacade facade = BillImportDetailFacade.Instance;
		protected static BillImportDetailBO instance = new BillImportDetailBO();

		protected BillImportDetailBO()
		{
			this.baseFacade = facade;
		}

		public static BillImportDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	