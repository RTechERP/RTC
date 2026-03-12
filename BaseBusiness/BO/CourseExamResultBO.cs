
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseExamResultBO : BaseBO
	{
		private CourseExamResultFacade facade = CourseExamResultFacade.Instance;
		protected static CourseExamResultBO instance = new CourseExamResultBO();

		protected CourseExamResultBO()
		{
			this.baseFacade = facade;
		}

		public static CourseExamResultBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	