
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillImportFacade : BaseFacade
	{
		protected static BillImportFacade instance = new BillImportFacade(new BillImportModel());
		protected BillImportFacade(BillImportModel model) : base(model)
		{
		}
		public static BillImportFacade Instance
		{
			get { return instance; }
		}
		protected BillImportFacade():base() 
		{ 
		} 
	
	}
}
	