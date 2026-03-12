
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillExportDetailFacade : BaseFacade
	{
		protected static BillExportDetailFacade instance = new BillExportDetailFacade(new BillExportDetailModel());
		protected BillExportDetailFacade(BillExportDetailModel model) : base(model)
		{
		}
		public static BillExportDetailFacade Instance
		{
			get { return instance; }
		}
		protected BillExportDetailFacade():base() 
		{ 
		} 
	
	}
}
	