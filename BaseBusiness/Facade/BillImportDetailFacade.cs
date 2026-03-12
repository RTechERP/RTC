
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillImportDetailFacade : BaseFacade
	{
		protected static BillImportDetailFacade instance = new BillImportDetailFacade(new BillImportDetailModel());
		protected BillImportDetailFacade(BillImportDetailModel model) : base(model)
		{
		}
		public static BillImportDetailFacade Instance
		{
			get { return instance; }
		}
		protected BillImportDetailFacade():base() 
		{ 
		} 
	
	}
}
	