
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseQuestionFacade : BaseFacade
	{
		protected static CourseQuestionFacade instance = new CourseQuestionFacade(new CourseQuestionModel());
		protected CourseQuestionFacade(CourseQuestionModel model) : base(model)
		{
		}
		public static CourseQuestionFacade Instance
		{
			get { return instance; }
		}
		protected CourseQuestionFacade():base() 
		{ 
		} 
	
	}
}
	