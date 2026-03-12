
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class InvoiceLinkFacade : BaseFacade
	{
		protected static InvoiceLinkFacade instance = new InvoiceLinkFacade(new InvoiceLinkModel());
		protected InvoiceLinkFacade(InvoiceLinkModel model) : base(model)
		{
		}
		public static InvoiceLinkFacade Instance
		{
			get { return instance; }
		}
		protected InvoiceLinkFacade():base() 
		{ 
		} 
	
	}
}
	