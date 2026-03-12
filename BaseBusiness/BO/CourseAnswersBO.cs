
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseAnswersBO : BaseBO
	{
		private CourseAnswersFacade facade = CourseAnswersFacade.Instance;
		protected static CourseAnswersBO instance = new CourseAnswersBO();

		protected CourseAnswersBO()
		{
			this.baseFacade = facade;
		}

		public static CourseAnswersBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	