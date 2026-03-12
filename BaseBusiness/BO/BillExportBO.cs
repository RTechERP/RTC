
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillExportBO : BaseBO
	{
		private BillExportFacade facade = BillExportFacade.Instance;
		protected static BillExportBO instance = new BillExportBO();

		protected BillExportBO()
		{
			this.baseFacade = facade;
		}

		public static BillExportBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	