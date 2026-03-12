
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseExamResultDetailFacade : BaseFacade
	{
		protected static CourseExamResultDetailFacade instance = new CourseExamResultDetailFacade(new CourseExamResultDetailModel());
		protected CourseExamResultDetailFacade(CourseExamResultDetailModel model) : base(model)
		{
		}
		public static CourseExamResultDetailFacade Instance
		{
			get { return instance; }
		}
		protected CourseExamResultDetailFacade():base() 
		{ 
		} 
	
	}
}
	