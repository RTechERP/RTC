
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseExamResultFacade : BaseFacade
	{
		protected static CourseExamResultFacade instance = new CourseExamResultFacade(new CourseExamResultModel());
		protected CourseExamResultFacade(CourseExamResultModel model) : base(model)
		{
		}
		public static CourseExamResultFacade Instance
		{
			get { return instance; }
		}
		protected CourseExamResultFacade():base() 
		{ 
		} 
	
	}
}
	