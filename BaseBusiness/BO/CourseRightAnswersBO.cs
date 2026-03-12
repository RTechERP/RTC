
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseRightAnswersBO : BaseBO
	{
		private CourseRightAnswersFacade facade = CourseRightAnswersFacade.Instance;
		protected static CourseRightAnswersBO instance = new CourseRightAnswersBO();

		protected CourseRightAnswersBO()
		{
			this.baseFacade = facade;
		}

		public static CourseRightAnswersBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	