
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillExportDetailSerialNumberFacade : BaseFacade
	{
		protected static BillExportDetailSerialNumberFacade instance = new BillExportDetailSerialNumberFacade(new BillExportDetailSerialNumberModel());
		protected BillExportDetailSerialNumberFacade(BillExportDetailSerialNumberModel model) : base(model)
		{
		}
		public static BillExportDetailSerialNumberFacade Instance
		{
			get { return instance; }
		}
		protected BillExportDetailSerialNumberFacade():base() 
		{ 
		} 
	
	}
}
	