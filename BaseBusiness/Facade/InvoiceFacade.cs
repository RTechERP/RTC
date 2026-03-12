
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class InvoiceFacade : BaseFacade
	{
		protected static InvoiceFacade instance = new InvoiceFacade(new InvoiceModel());
		protected InvoiceFacade(InvoiceModel model) : base(model)
		{
		}
		public static InvoiceFacade Instance
		{
			get { return instance; }
		}
		protected InvoiceFacade():base() 
		{ 
		} 
	
	}
}
	