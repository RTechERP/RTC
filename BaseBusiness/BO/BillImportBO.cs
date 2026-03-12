
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BillImportBO : BaseBO
	{
		private BillImportFacade facade = BillImportFacade.Instance;
		protected static BillImportBO instance = new BillImportBO();

		protected BillImportBO()
		{
			this.baseFacade = facade;
		}

		public static BillImportBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	