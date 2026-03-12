
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillExportTechnicalFacade : BaseFacade
	{
		protected static BillExportTechnicalFacade instance = new BillExportTechnicalFacade(new BillExportTechnicalModel());
		protected BillExportTechnicalFacade(BillExportTechnicalModel model) : base(model)
		{
		}
		public static BillExportTechnicalFacade Instance
		{
			get { return instance; }
		}
		protected BillExportTechnicalFacade():base() 
		{ 
		} 
	
	}
}
	