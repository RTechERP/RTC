
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseExamPracticeBO : BaseBO
	{
		private CourseExamPracticeFacade facade = CourseExamPracticeFacade.Instance;
		protected static CourseExamPracticeBO instance = new CourseExamPracticeBO();

		protected CourseExamPracticeBO()
		{
			this.baseFacade = facade;
		}

		public static CourseExamPracticeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	