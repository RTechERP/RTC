
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseExamPracticeFacade : BaseFacade
	{
		protected static CourseExamPracticeFacade instance = new CourseExamPracticeFacade(new CourseExamPracticeModel());
		protected CourseExamPracticeFacade(CourseExamPracticeModel model) : base(model)
		{
		}
		public static CourseExamPracticeFacade Instance
		{
			get { return instance; }
		}
		protected CourseExamPracticeFacade():base() 
		{ 
		} 
	
	}
}
	