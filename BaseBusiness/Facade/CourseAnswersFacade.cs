
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class CourseAnswersFacade : BaseFacade
	{
		protected static CourseAnswersFacade instance = new CourseAnswersFacade(new CourseAnswersModel());
		protected CourseAnswersFacade(CourseAnswersModel model) : base(model)
		{
		}
		public static CourseAnswersFacade Instance
		{
			get { return instance; }
		}
		protected CourseAnswersFacade():base() 
		{ 
		} 
	
	}
}
	