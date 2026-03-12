
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CourseExamResultDetailBO : BaseBO
	{
		private CourseExamResultDetailFacade facade = CourseExamResultDetailFacade.Instance;
		protected static CourseExamResultDetailBO instance = new CourseExamResultDetailBO();

		protected CourseExamResultDetailBO()
		{
			this.baseFacade = facade;
		}

		public static CourseExamResultDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	