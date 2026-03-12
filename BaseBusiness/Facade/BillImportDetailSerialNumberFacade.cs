
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BillImportDetailSerialNumberFacade : BaseFacade
	{
		protected static BillImportDetailSerialNumberFacade instance = new BillImportDetailSerialNumberFacade(new BillImportDetailSerialNumberModel());
		protected BillImportDetailSerialNumberFacade(BillImportDetailSerialNumberModel model) : base(model)
		{
		}
		public static BillImportDetailSerialNumberFacade Instance
		{
			get { return instance; }
		}
		protected BillImportDetailSerialNumberFacade():base() 
		{ 
		} 
	
	}
}
	