
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillImportTechnicalFacade : BaseFacade
	{
		protected static BillImportTechnicalFacade instance = new BillImportTechnicalFacade(new BillImportTechnicalModel());
		protected BillImportTechnicalFacade(BillImportTechnicalModel model) : base(model)
		{
		}
		public static BillImportTechnicalFacade Instance
		{
			get { return instance; }
		}
		protected BillImportTechnicalFacade():base() 
		{ 
		} 
	
	}
}
	