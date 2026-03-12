
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseExamEvaluateFacade : BaseFacade
	{
		protected static CourseExamEvaluateFacade instance = new CourseExamEvaluateFacade(new CourseExamEvaluateModel());
		protected CourseExamEvaluateFacade(CourseExamEvaluateModel model) : base(model)
		{
		}
		public static CourseExamEvaluateFacade Instance
		{
			get { return instance; }
		}
		protected CourseExamEvaluateFacade():base() 
		{ 
		} 
	
	}
}
	