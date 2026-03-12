
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseExamBO : BaseBO
	{
		private CourseExamFacade facade = CourseExamFacade.Instance;
		protected static CourseExamBO instance = new CourseExamBO();

		protected CourseExamBO()
		{
			this.baseFacade = facade;
		}

		public static CourseExamBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	