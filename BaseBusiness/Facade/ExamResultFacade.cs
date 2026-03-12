
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ExamResultFacade : BaseFacade
	{
		protected static ExamResultFacade instance = new ExamResultFacade(new ExamResultModel());
		protected ExamResultFacade(ExamResultModel model) : base(model)
		{
		}
		public static ExamResultFacade Instance
		{
			get { return instance; }
		}
		protected ExamResultFacade():base() 
		{ 
		} 
	
	}
}
	