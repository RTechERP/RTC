
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class PartSummaryDetailFacade : BaseFacade
	{
		protected static PartSummaryDetailFacade instance = new PartSummaryDetailFacade(new PartSummaryDetailModel());
		protected PartSummaryDetailFacade(PartSummaryDetailModel model) : base(model)
		{
		}
		public static PartSummaryDetailFacade Instance
		{
			get { return instance; }
		}
		protected PartSummaryDetailFacade():base() 
		{ 
		} 
	
	}
}
	