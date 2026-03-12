
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillImportTechnicalLogFacade : BaseFacade
	{
		protected static BillImportTechnicalLogFacade instance = new BillImportTechnicalLogFacade(new BillImportTechnicalLogModel());
		protected BillImportTechnicalLogFacade(BillImportTechnicalLogModel model) : base(model)
		{
		}
		public static BillImportTechnicalLogFacade Instance
		{
			get { return instance; }
		}
		protected BillImportTechnicalLogFacade():base() 
		{ 
		} 
	
	}
}
	