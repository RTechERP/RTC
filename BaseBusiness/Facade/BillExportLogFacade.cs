
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillExportLogFacade : BaseFacade
	{
		protected static BillExportLogFacade instance = new BillExportLogFacade(new BillExportLogModel());
		protected BillExportLogFacade(BillExportLogModel model) : base(model)
		{
		}
		public static BillExportLogFacade Instance
		{
			get { return instance; }
		}
		protected BillExportLogFacade():base() 
		{ 
		} 
	
	}
}
	