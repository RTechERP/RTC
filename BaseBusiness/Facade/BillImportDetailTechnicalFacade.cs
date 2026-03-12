
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillImportDetailTechnicalFacade : BaseFacade
	{
		protected static BillImportDetailTechnicalFacade instance = new BillImportDetailTechnicalFacade(new BillImportDetailTechnicalModel());
		protected BillImportDetailTechnicalFacade(BillImportDetailTechnicalModel model) : base(model)
		{
		}
		public static BillImportDetailTechnicalFacade Instance
		{
			get { return instance; }
		}
		protected BillImportDetailTechnicalFacade():base() 
		{ 
		} 
	
	}
}
	