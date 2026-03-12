
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillExportDetailTechnicalFacade : BaseFacade
	{
		protected static BillExportDetailTechnicalFacade instance = new BillExportDetailTechnicalFacade(new BillExportDetailTechnicalModel());
		protected BillExportDetailTechnicalFacade(BillExportDetailTechnicalModel model) : base(model)
		{
		}
		public static BillExportDetailTechnicalFacade Instance
		{
			get { return instance; }
		}
		protected BillExportDetailTechnicalFacade():base() 
		{ 
		} 
	
	}
}
	