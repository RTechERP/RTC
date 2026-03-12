
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseRightAnswersFacade : BaseFacade
	{
		protected static CourseRightAnswersFacade instance = new CourseRightAnswersFacade(new CourseRightAnswersModel());
		protected CourseRightAnswersFacade(CourseRightAnswersModel model) : base(model)
		{
		}
		public static CourseRightAnswersFacade Instance
		{
			get { return instance; }
		}
		protected CourseRightAnswersFacade():base() 
		{ 
		} 
	
	}
}
	