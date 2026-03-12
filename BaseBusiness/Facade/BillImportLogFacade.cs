
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillImportLogFacade : BaseFacade
	{
		protected static BillImportLogFacade instance = new BillImportLogFacade(new BillImportLogModel());
		protected BillImportLogFacade(BillImportLogModel model) : base(model)
		{
		}
		public static BillImportLogFacade Instance
		{
			get { return instance; }
		}
		protected BillImportLogFacade():base() 
		{ 
		} 
	
	}
}
	