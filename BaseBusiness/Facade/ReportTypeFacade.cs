
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ReportTypeFacade : BaseFacade
	{
		protected static ReportTypeFacade instance = new ReportTypeFacade(new ReportTypeModel());
		protected ReportTypeFacade(ReportTypeModel model) : base(model)
		{
		}
		public static ReportTypeFacade Instance
		{
			get { return instance; }
		}
		protected ReportTypeFacade():base() 
		{ 
		} 
	
	}
}
	