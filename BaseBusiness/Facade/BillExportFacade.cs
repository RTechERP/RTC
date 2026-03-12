
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillExportFacade : BaseFacade
	{
		protected static BillExportFacade instance = new BillExportFacade(new BillExportModel());
		protected BillExportFacade(BillExportModel model) : base(model)
		{
		}
		public static BillExportFacade Instance
		{
			get { return instance; }
		}
		protected BillExportFacade():base() 
		{ 
		} 
	
	}
}
	