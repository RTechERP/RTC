
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamResultDetailFacade : BaseFacade
	{
		protected static ExamResultDetailFacade instance = new ExamResultDetailFacade(new ExamResultDetailModel());
		protected ExamResultDetailFacade(ExamResultDetailModel model) : base(model)
		{
		}
		public static ExamResultDetailFacade Instance
		{
			get { return instance; }
		}
		protected ExamResultDetailFacade():base() 
		{ 
		} 
	
	}
}
	