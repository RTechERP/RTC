
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseExamFacade : BaseFacade
	{
		protected static CourseExamFacade instance = new CourseExamFacade(new CourseExamModel());
		protected CourseExamFacade(CourseExamModel model) : base(model)
		{
		}
		public static CourseExamFacade Instance
		{
			get { return instance; }
		}
		protected CourseExamFacade():base() 
		{ 
		} 
	
	}
}
	